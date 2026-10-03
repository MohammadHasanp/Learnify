using CoreModule.Application.CourseStudents.Add;
using CoreModule.Facade.Courses;
using CoreModule.Facade.Orders;
using CoreModule.Query.Orders.DTOs;
using Learnify.Web.Infrastructure;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionModule.Domain;
using TransactionModule.Enum;
using TransactionModule.Services;
using TransactionModule.Services.DTOs.Commands;
using TransactionModule.Services.Zarinpal;

namespace Learnify.Web.Controllers;

public class TransactionController(IZarinPalService zarinPalService, ITransactionService transactionService, ILogger<TransactionController> logger,
    ICourseFacade courseFacade, IOrderFacade orderFacade) : BaseController
{
    //private readonly Payment _payment;
    //private readonly Authority _authority;
    //private readonly Transactions _transactions;
    //var expose = new Expose();
    //_payment = expose.CreatePayment();
    //_authority = expose.CreateAuthority();
    //_transactions = expose.CreateTransactions();

    [Authorize]
    public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command)
    {
        var order = await orderFacade.GetCurrentOrder(User.GetUserId());
        if (order == null)
            throw new InvalidDataException("Order Is Null");

        command.PaymentAmount = order.TotalPrice;
        command.UserId = User.GetUserId();
        command.PaymentGateway = PaymentGateway.ZarinPal;
        command.LinkId = order.Id;
        var transactionId = await transactionService.CreateTransaction(command);

        var payment = await zarinPalService.CreatePaymentRequest(
            command.PaymentAmount, $"پرداخت تراکنش شماره {transactionId}",
            $"https://localhost:7058/Transaction/Zarinpal-Verify/{transactionId}");

        if (payment.Status == 100)
        {
            return Redirect(payment.GateWayUrl);
        }
        return Redirect("/");
    }


    [Route("/Transaction/Zarinpal-Verify/{id}")]
    [Authorize]
    public async Task<IActionResult> ZarinPalVerify(string authority, string status, Guid id)
    {
        if (string.IsNullOrWhiteSpace(status) || status.ToLower() == "nok" || string.IsNullOrWhiteSpace(authority))
        {
            await PayError(new TransactionPaymentErrorCommand()
            {
                Authority = authority,
                RefId = 0,
                ErrorMessage = "Payment Canceled Or Canceled With Error - Status = NOK",
                TransactionId = id,
                Canceled = true
            });
            return View("EndTransaction");
        }

        var transaction = await transactionService.GetTransactionById(id);

        if (transaction == null)
            return NotFound();

        if (User.GetUserId() != transaction.UserId)
            return BadRequest();

        if (transaction.Status == TransactionStatus.PaymentSuccess)
            return BadRequest();

        try
        {
            //await FinallyTransaction(transaction);

            var result = await zarinPalService.CreateVerificationRequest(authority, transaction.PaymentAmount);
            if (result.Status == 100)
            {
                TempData["Success"] = true;
                await transactionService.PaymentSuccess(new TransactionPaymentSuccessCommand()
                {
                    Authority = authority,
                    CardPen = result.CardPan,
                    RefId = result.RefId,
                    TransactionId = id
                });
                await FinallyTransaction(transaction);
            }
            else
            {
                await PayError(new TransactionPaymentErrorCommand
                {
                    RefId = result.RefId,
                    TransactionId = id,
                    ErrorMessage = result.Message,
                    Authority = authority
                });
            }
        }
        catch (Exception e)
        {
            await PayError(e, id, authority);
        }
        return View("EndTransaction");
    }

    #region Utilities

    private async Task FinallyTransaction(Transaction transaction)
    {
        switch (transaction.TransactionFor)
        {
            case TransactionFor.CourseOrder:
                {
                    var order = await orderFacade.GetCurrentOrder(transaction.UserId);
                    if (order?.TotalPrice != transaction.PaymentAmount)
                        throw new InvalidDataException("اطلاعات پرداخت با اطلاعات سفارش همخوانی ندارند");

                    await orderFacade.FinallyOrder(order.Id);
                    await AddCourseStudent(order);
                    break;
                }

        }
    }

    private async Task PayError(Exception e, Guid id, string authority)
    {
        TempData["Error"] = true;
        await transactionService.PaymentError(new TransactionPaymentErrorCommand
        {
            RefId = 0,
            TransactionId = id,
            ErrorMessage = e.Message,
            Authority = authority
        });
        logger.LogError(e.Message, e);
    }
    private async Task PayError(TransactionPaymentErrorCommand command)
    {
        TempData["Error"] = true;
        await transactionService.PaymentError(command);
    }

    private async Task AddCourseStudent(OrderDto order)
    {
        foreach (var detail in order.OrderItems)
        {
            await courseFacade.AddCourseStudent(detail.CourseId, order.UserId);
        }
    }
    #endregion
}
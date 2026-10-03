using CoreModule.Application.Orders.AddItem;
using CoreModule.Application.Orders.RemoveOrder;
using CoreModule.Facade.Orders;
using CoreModule.Query.Orders.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionModule.Enum;
using TransactionModule.Services.DTOs.Commands;

namespace Learnify.Web.Pages
{
    [Authorize]
    public class CartModel(IOrderFacade orderFacade) : BaseRazorPage
    {
        public OrderDto? Order { get; set; }
        public async Task OnGet()
        {
            Order = await orderFacade.GetCurrentOrder(User.GetUserId());
        }

        public IActionResult OnPost()
        {
            return RedirectToAction("CreateTransaction", "Transaction", new CreateTransactionCommand()
            {
                LinkId = Guid.NewGuid(),
                PaymentAmount = 0,
                UserId = User.GetUserId(),
                PaymentGateway = PaymentGateway.ZarinPal,
                TransactionFor = TransactionFor.CourseOrder
            });
        }
        public async Task<IActionResult> OnGetAddItem(Guid courseId)
        {
            var result = await orderFacade.AddItem(new AddOrderItemCommand(User.GetUserId(), courseId));
            return RedirectAndShowAlert(result, RedirectToPage("/cart"));
        }
        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() => orderFacade.RemoveItem(new RemoveOrderItemCommand(User.GetUserId(), id)));
        }
    }
}

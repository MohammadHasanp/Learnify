using Common.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using TransactionModule.Context;
using TransactionModule.Domain;
using TransactionModule.Enum;
using TransactionModule.Services.DTOs.Commands;
using TransactionModule.Services.DTOs.Queries;

namespace TransactionModule.Services;

public class TransactionService(TransactionContext context) : ITransactionService
{
    //private readonly IUserAcl _userAcl;
    //_userAcl = userAcl;

    public async Task<Guid> CreateTransaction(CreateTransactionCommand command)
    {
        var transaction = new Transaction(command.UserId, command.PaymentAmount, command.TransactionFor, command.LinkId, command.PaymentGateway);
        await context.AddAsync(transaction);
        await context.SaveChangesAsync();
        return transaction.Id;
    }

    public async Task PaymentSuccess(TransactionPaymentSuccessCommand command)
    {
        var transaction = await GetTransactionById(command.TransactionId);
        if (transaction != null)
        {
            transaction.PaymentSuccess(command.Authority, command.CardPen, command.RefId);
            context.Update(transaction);
            await context.SaveChangesAsync();
        }
    }

    public async Task PaymentError(TransactionPaymentErrorCommand command)
    {
        var transaction = await GetTransactionById(command.TransactionId);

        if (transaction != null)
        {
            transaction.PaymentError(command.ErrorMessage, command.RefId, command.Authority, command.Canceled);
            context.Update(transaction);
            await context.SaveChangesAsync();
        }
    }
    public async Task<Transaction?> GetTransactionById(Guid id)
    {
        return await context.UserTransactions.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TransactionFilterDto> GetTransactionsByFilter(TransactionFilterParams queryParams)
    {
        var result = context.UserTransactions
            .OrderByDescending(d => d.CreateDate).AsQueryable();


        if (queryParams.TransactionFor != null)
            result = result.Where(r => r.TransactionFor == queryParams.TransactionFor);

        if (queryParams.Status != null && queryParams.Status != TransactionStatus.Pending)
            result = result.Where(r => r.Status == queryParams.Status);

        if (queryParams.UserId != null)
            result = result.Where(r => r.UserId == queryParams.UserId);

        if (queryParams.StartDate != null)
        {
            DateTime startDate = (DateTime)queryParams.StartDate;
            result = result.Where(r => (r.PaymentDate != null && r.PaymentDate >= queryParams.StartDate) || r.CreateDate >= startDate);
        }

        if (queryParams.EndDate != null)
        {
            var endDate = (DateTime)queryParams.EndDate;
            result = result.Where(r => r.PaymentDate != null && r.PaymentDate <= queryParams.EndDate || r.CreateDate <= endDate);
        }

        var skip = (queryParams.PageId - 1) * queryParams.Take;
        var totalPrice = 0;

        var model = new TransactionFilterDto()
        {
            FilterParams = queryParams,
            Datas = await result.Skip(skip).Take(queryParams.Take).Select(s =>
                new TransactionDto
                {
                    PaymentLinkId = s.PaymentLinkId,
                    TransactionFor = s.TransactionFor,
                    RefId = s.RefId,
                    UserId = s.UserId,
                    Status = s.Status,
                    PaymentDate = s.PaymentDate,
                    Authority = s.Authority!,
                    CardPan = s.CardPan!,
                    CreationDate = s.CreateDate,
                    Id = s.Id,
                    PaymentAmount = s.PaymentAmount,
                    PaymentErrorMessage = s.PaymentErrorMessage!,
                    PaymentGateWay = s.PaymentGateway!,
                    //UserFullName = _userAcl.GetUserFullName(s.UserId)
                }).ToListAsync(),

        };
        model.GeneratePaging(result, queryParams.Take, queryParams.PageId);


        var paymentTake = 200;
        var pageCount = (int)Math.Ceiling(model.EntityCount / (double)paymentTake);
        for (var i = 1; i <= pageCount; i++)
        {
            var paymentSkip = (i - 1) * paymentTake;
            totalPrice += await result
                .Where(r => r.Status == TransactionStatus.PaymentSuccess)
                .Skip(paymentSkip)
                .Take(paymentTake)
                .SumAsync(s => s.PaymentAmount);

        }
        model.TotalPayment = totalPrice;
        return model;
    }

    public async Task<int> GetCancelTransactionsCount(string stDate, string eDate, TransactionFor? transactionFor, TransactionStatus status)
    {
        var result = context.UserTransactions.Where(p => p.Status == TransactionStatus.PaymentError).AsQueryable();

        if (transactionFor != null)
        {
            result = result.Where(r => r.TransactionFor == transactionFor);
        }

        if (status != TransactionStatus.Pending)
            result = result.Where(r => r.Status == status);

        if (!string.IsNullOrEmpty(stDate))
        {

            DateTime startDate = stDate.ToMiladi();
            result = result.Where(r => (r.PaymentDate != null && r.PaymentDate >= stDate.ToMiladi()) || r.CreateDate >= startDate);
        }

        if (!string.IsNullOrEmpty(eDate))
        {
            var endDate = eDate.ToMiladi();
            result = result.Where(r => (r.PaymentDate != null && r.PaymentDate <= eDate.ToMiladi()) || r.CreateDate <= endDate);
        }

        var res = await result.ToListAsync();

        return res.Count;
    }
}
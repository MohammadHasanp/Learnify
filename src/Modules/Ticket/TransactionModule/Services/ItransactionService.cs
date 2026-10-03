using TransactionModule.Domain;
using TransactionModule.Enum;
using TransactionModule.Services.DTOs.Commands;
using TransactionModule.Services.DTOs.Queries;

namespace TransactionModule.Services;

public interface ITransactionService
{
    #region Commands

    Task<Guid> CreateTransaction(CreateTransactionCommand command);
    Task PaymentSuccess(TransactionPaymentSuccessCommand command);
    Task PaymentError(TransactionPaymentErrorCommand command);

    #endregion

    #region Queries
    Task<Transaction?> GetTransactionById(Guid id);
    Task<TransactionFilterDto> GetTransactionsByFilter(TransactionFilterParams queryParams);
    Task<int> GetCancelTransactionsCount(string stDate, string eDate, TransactionFor? transactionFor, TransactionStatus status);
    #endregion
}
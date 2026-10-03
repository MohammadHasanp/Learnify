using TransactionModule.Enum;

namespace TransactionModule.Services.DTOs.Commands
{
    public class CreateTransactionCommand
    {
        public Guid UserId { get; set; }
        public int PaymentAmount { get; set; }
        public Guid LinkId { get; set; }
        public PaymentGateway PaymentGateway { get; set; }
        public TransactionFor TransactionFor { get; set; }
    }

    public class TransactionPaymentErrorCommand
    {
        public Guid TransactionId { get; set; }
        public string ErrorMessage { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; }
        public bool Canceled { get; set; } = false;
    }

    public class TransactionPaymentSuccessCommand
    {
        public Guid TransactionId { get; set; }
        public string Authority { get; set; } = null!;
        public long RefId { get; set; }
        public string CardPen { get; set; } = null!;
        public string SuccessCallBack { get; set; } = null!;
    }
}

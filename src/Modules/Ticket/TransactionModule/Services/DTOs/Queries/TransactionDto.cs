using Common.Query;
using TransactionModule.Enum;

namespace TransactionModule.Services.DTOs.Queries;

public class TransactionDto : BaseDto
{
    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = null!;
    public int PaymentAmount { get; set; }
    public Guid PaymentLinkId { get; set; }
    public long? RefId { get; set; }
    public string Authority { get; set; } = null!;
    public string CardPan { get; set; } = null!;
    public string PaymentErrorMessage { get; set; } = null!;
    public string PaymentGateWay { get; set; } = null!;
    public TransactionStatus Status { get; set; }
    public TransactionFor TransactionFor { get; set; }
    public DateTime? PaymentDate { get; set; }
}

public class TransactionFilterParams : BaseFilterParam
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TransactionStatus? Status { get; set; }
    public TransactionFor? TransactionFor { get; set; }
    public Guid? UserId { get; set; }
}

public class TransactionFilterDto : BaseFilter<TransactionDto,TransactionFilterParams>
{
    public int TotalPayment { get; set; }
}
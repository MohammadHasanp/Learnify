namespace CoreModule.Domain.Orders;

public interface IOrderDomainService
{
    public Task<int> GetCoursePriceById(Guid courseId);
}
using Common.Domain.Repository;
using CoreModule.Domain.Orders.Models;

namespace CoreModule.Domain.Orders.Repository;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Models.Order?> GetCurrentOrderByUserId(Guid userId);
}
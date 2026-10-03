using Common.Infrastructure;
using CoreModule.Domain.Orders.Models;
using CoreModule.Domain.Orders.Repository;
using CoreModule.Infrastructure.Persistent._Context;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Orders;

class OrderRepository(CoreModuleEfContext context) : BaseRepository<Order, CoreModuleEfContext>(context), IOrderRepository
{
    public async Task<Order?> GetCurrentOrderByUserId(Guid userId)
    {
        return await Context.Orders.AsTracking().FirstOrDefaultAsync(f => f.IsPay == false && f.UserId == userId);
    }
}
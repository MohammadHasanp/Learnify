using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Orders.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Orders.GetCurrent;

public record GetCurrentOrderQuery(Guid UserId) : IQuery<OrderDto?>;



class GetCurrentOrderQueryHandler(QueryContext context) : IQueryHandler<GetCurrentOrderQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetCurrentOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(c => c.OrderItems)
            .ThenInclude(c => c.Course.Teacher.Users)
            .Include(c => c.User)
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.IsPay == false
                , cancellationToken: cancellationToken);

        return OrderMapper.MapOrder(order);
    }
}
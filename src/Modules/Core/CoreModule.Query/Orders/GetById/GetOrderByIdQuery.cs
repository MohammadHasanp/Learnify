using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Orders.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Orders.GetById;

public record GetOrderByIdQuery(Guid Id) : IQuery<OrderDto?>;

class GetOrderByIdQueryHandler(QueryContext context) : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(c => c.OrderItems)
            .ThenInclude(c => c.Course)
            .Include(c => c.User)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken);

        return OrderMapper.MapOrder(order);
    }
}
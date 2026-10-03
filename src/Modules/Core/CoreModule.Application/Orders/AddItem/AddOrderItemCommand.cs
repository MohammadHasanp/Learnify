using Common.Application;
using CoreModule.Domain.Orders;
using CoreModule.Domain.Orders.Models;
using CoreModule.Domain.Orders.Repository;

namespace CoreModule.Application.Orders.AddItem;

public record AddOrderItemCommand(Guid UserId, Guid CourseId) : IBaseCommand;


public class AddOrderItemCommandHandler(IOrderRepository orderRepository, IOrderDomainService domainService) : IBaseCommandHandler<AddOrderItemCommand>
{
    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetCurrentOrderByUserId(request.UserId);
        if (order == null)
        {
            var newOrder = new Order(request.UserId);
            await newOrder.AddItem(request.CourseId, domainService);
            await orderRepository.AddAsync(newOrder);
        }
        else
        {
            await order.AddItem(request.CourseId, domainService);
        }

        await orderRepository.Save();
        return OperationResult.Success();
    }
}
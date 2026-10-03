using Common.Application;
using CoreModule.Application.Orders.AddItem;
using CoreModule.Application.Orders.FinallyOrder;
using CoreModule.Application.Orders.RemoveOrder;
using CoreModule.Query.Orders.DTOs;
using CoreModule.Query.Orders.GetCurrent;
using MediatR;

namespace CoreModule.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> AddItem(AddOrderItemCommand command);
    Task<OperationResult> RemoveItem(RemoveOrderItemCommand command);
    Task<OperationResult> FinallyOrder(Guid orderId);

    Task<OrderDto?> GetCurrentOrder(Guid userId);
}

class OrderFacade(IMediator mediator) : IOrderFacade
{
    public async Task<OperationResult> AddItem(AddOrderItemCommand command)
    {
        return await mediator.Send(command);
    }

    public async Task<OperationResult> RemoveItem(RemoveOrderItemCommand command)
    {
        return await mediator.Send(command);

    }

    public async Task<OperationResult> FinallyOrder(Guid orderId)
    {
        return await mediator.Send(new FinallyOrderCommand()
        {
            OrderId = orderId
        });
    }

    public async Task<OrderDto?> GetCurrentOrder(Guid userId)
    {
        return await mediator.Send(new GetCurrentOrderQuery(userId));

    }
}
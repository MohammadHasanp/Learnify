using Common.Application;
using CoreModule.Domain.Orders.Repository;

namespace CoreModule.Application.Orders.RemoveOrder;

public record RemoveOrderItemCommand(Guid UserId, Guid Id) : IBaseCommand;


public class RemoveOrderItemCommandHandler(IOrderRepository orderRepository) : IBaseCommandHandler<RemoveOrderItemCommand>
{
    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetCurrentOrderByUserId(request.UserId);
        if (order == null)
        {
            return OperationResult.NotFound();
        }

        order.RemoveItem(request.Id);
        await orderRepository.Save();
        return OperationResult.Success();
    }
}
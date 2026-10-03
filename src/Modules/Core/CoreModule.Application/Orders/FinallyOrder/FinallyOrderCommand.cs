using Common.Application;
using CoreModule.Domain.Orders.Repository;

namespace CoreModule.Application.Orders.FinallyOrder;

public class FinallyOrderCommand : IBaseCommand
{
    public Guid OrderId { get; set; }
}
class FinallyOrderCommandHandler(IOrderRepository repository) : IBaseCommandHandler<FinallyOrderCommand>
{
    public async Task<OperationResult> Handle(FinallyOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetTracking(request.OrderId);
        if (order == null)
        {
            return OperationResult.NotFound();
        }
        order.FinallyOrder();
        await repository.Save();
        return OperationResult.Success();
    }
}
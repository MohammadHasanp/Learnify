using Common.Application;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Teachers.Repository;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.Teachers.RejectedRequest;

public class RejectedTeacherRequestHandler(ITeacherRepository repository, IEventBus eventBus) : IBaseCommandHandler<RejectedTeacherRequestCommand>
{
    public async Task<OperationResult> Handle(RejectedTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await repository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();

        repository.Delete(teacher);
        await repository.Save();
        await eventBus.Publish(new NewNotificationIntegrationEvent()
        {
            Title = "درخواست مدرسی شما رد شد سریع خداحافظی کن!",
            Description =
              $"کاربر گرامی درخواست مدرسی شما به دلیل زیر رد شد :<hr/><p>{request.Description}</p>",
            UserId = teacher.UserId,
        }, null, Exchanges.NotificationExchange, ExchangeType.Fanout);
        return OperationResult.Success();
    }
}
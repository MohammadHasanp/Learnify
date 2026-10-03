using Common.Application;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Teachers.Repository;
using RabbitMQ.Client;

namespace CoreModule.Application.Teachers.AcceptedRequest;

public class AcceptedTeacherRequestHandler(ITeacherRepository repository, IEventBus eventBus) : IBaseCommandHandler<AcceptedTeacherRequestCommand>
{
    public async Task<OperationResult> Handle(AcceptedTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await repository.GetTracking(request.TeacherId);

        if (teacher == null)
            return OperationResult.NotFound();

        teacher.AcceptRequest();
        await repository.Save();
        await eventBus.Publish(new NewNotificationIntegrationEvent()
        {
            Description = "تبریک! پنل مدرسی شما در این لینک فعال است <hr/><a href='/profile/teacher/courses' target='_blank'>ورود</a>",
            Title = "درخواست مدرسی شما تایید شد ",
            UserId = teacher.UserId,
        }, null, Exchanges.NotificationExchange, ExchangeType.Fanout);

        return OperationResult.Success();
    }
}
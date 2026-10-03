using Common.EventBus.Abstractions;
using Common.EventBus.RabbitMQ;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.EntityHelpers;
using CoreModule.Domain.Orders.Repository;
using CoreModule.Domain.Teachers.Repository;
using CoreModule.Infrastructure.Handlers;
using CoreModule.Infrastructure.Orders;
using CoreModule.Infrastructure.Persistent._Context;
using CoreModule.Infrastructure.Persistent.CourseCategories;
using CoreModule.Infrastructure.Persistent.Courses;
using CoreModule.Infrastructure.Persistent.EntityHelpers.CourseStudents;
using CoreModule.Infrastructure.Persistent.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Infrastructure;

public static class CoreModuleInfrastructureBootstrapper
{
    public static IServiceCollection RegisterDependency(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CoreModuleEfContext>(option => option.UseSqlServer(config.GetConnectionString("Core-Context")));

        services.AddHostedService<UserRegisteredEventHandler>();
        services.AddHostedService<UserEditedEventHandler>();

        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IEventBus, EventBusRabbitMq>();

        return services;
    }
}

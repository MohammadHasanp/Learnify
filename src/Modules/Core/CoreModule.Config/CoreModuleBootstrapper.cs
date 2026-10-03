using CoreModule.Application.Orders;
using CoreModule.Domain.Categories.Services;
using CoreModule.Domain.Orders;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.StorageServices;
using CoreModule.Application._Utilities;
using CoreModule.Application.CourseCategories.Create;
using CoreModule.Domain.Courses.Service;
using CoreModule.Domain.Teachers.Service;
using CoreModule.Facade;
using CoreModule.Infrastructure;
using CoreModule.Infrastructure.Persistent.CourseCategories.Services;
using CoreModule.Infrastructure.Persistent.Courses.Services;
using CoreModule.Infrastructure.Persistent.Teachers.Services;
using CoreModule.Query;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Config;

public static class CoreModuleBootstrapper
{
    public static IServiceCollection InitCoreModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CoreModuleDirectories>());
        services.AddValidatorsFromAssembly(typeof(CreateCourseCategoryCommand).Assembly);

        CoreModuleFacadeBootstrapper.RegisterDependency(services, config);
        CoreModuleInfrastructureBootstrapper.RegisterDependency(services, config);
        CoreModuleQueryBootstrapper.RegisterDependency(services, config);

        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IOrderDomainService, OrderDomainService>();
        services.AddScoped<ICourseCategoryService, CourseCategoryServide>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ILocalFileService, LocalFileService>();

        return services;
    }
}

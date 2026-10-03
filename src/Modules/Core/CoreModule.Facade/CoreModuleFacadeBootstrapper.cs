using CoreModule.Facade.CourseCategories;
using CoreModule.Facade.Courses;
using CoreModule.Facade.Orders;
using CoreModule.Facade.Teachers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Facade;

public static class CoreModuleFacadeBootstrapper
{
    public static IServiceCollection RegisterDependency(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICourseFacade, CourseFacade>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<ITeacherFaced, TeacherFacade>();
        services.AddScoped<ICourseCategoryFacade, CourseCategoryFacade>();

        return services;
    }
}


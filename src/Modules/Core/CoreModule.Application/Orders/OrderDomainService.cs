using CoreModule.Domain.Courses.Repository;
using CoreModule.Domain.Orders;

namespace CoreModule.Application.Orders;

public class OrderDomainService(ICourseRepository courseRepository) : IOrderDomainService
{
    public async Task<int> GetCoursePriceById(Guid courseId)
    {
        var course = await courseRepository.GetAsync(courseId);
        return course?.Price ?? 0;
    }
}
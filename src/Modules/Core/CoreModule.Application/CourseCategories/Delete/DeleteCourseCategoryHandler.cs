using Common.Application;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.CourseCategories.Delete;

public class DeleteCourseCategoryHandler(ICourseCategoryRepository repository) : IBaseCommandHandler<DeleteCourseCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository = repository;
    public async Task<OperationResult> Handle(DeleteCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.Id);
        if (category == null)
            return OperationResult.NotFound();

        await _repository.Delete(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}
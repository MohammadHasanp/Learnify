using Common.Application;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Categories.Services;

namespace CoreModule.Application.CourseCategories.Edit;

public class EditCourseCategoryHandler(ICourseCategoryRepository repository, ICourseCategoryService service) : IBaseCommandHandler<EditCourseCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository = repository;
    private readonly ICourseCategoryService _service = service;

    public async Task<OperationResult> Handle(EditCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.Id);
        if (category == null)
            return OperationResult.NotFound();

        category.Edit(request.Title, request.Slug, _service);
        await _repository.Save();
        return OperationResult.Success();
    }
}

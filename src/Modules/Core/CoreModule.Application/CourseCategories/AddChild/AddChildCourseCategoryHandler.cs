using Common.Application;
using CoreModule.Domain.Categories;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Categories.Services;

namespace CoreModule.Application.CourseCategories.AddChild;

public class AddChildCourseCategoryHandler(ICourseCategoryRepository repository, ICourseCategoryService service) : IBaseCommandHandler<AddChildCourseCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository = repository;
    private readonly ICourseCategoryService _service = service;
    public async Task<OperationResult> Handle(AddChildCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, request.ParentId, _service);
        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}
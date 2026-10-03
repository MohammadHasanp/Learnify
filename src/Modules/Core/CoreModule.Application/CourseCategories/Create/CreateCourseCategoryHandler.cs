using Common.Application;
using CoreModule.Domain.Categories;
using CoreModule.Domain.Categories.Repository;
using CoreModule.Domain.Categories.Services;

namespace CoreModule.Application.CourseCategories.Create;

public class CreateCourseCategoryHandler(ICourseCategoryRepository repository, ICourseCategoryService service) : IBaseCommandHandler<CreateCourseCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository = repository;
    private readonly ICourseCategoryService _service = service;
    public async Task<OperationResult> Handle(CreateCourseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, null, _service);
        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}
using Common.Application;
using CoreModule.Application.CourseCategories.AddChild;
using CoreModule.Application.CourseCategories.Create;
using CoreModule.Application.CourseCategories.Edit;
using CoreModule.Query.CourseCategories.DTOs;


namespace CoreModule.Facade.CourseCategories;

public interface ICourseCategoryFacade
{
    Task<OperationResult> Create(CreateCourseCategoryCommand command);
    Task<OperationResult> Edit(EditCourseCategoryCommand command);
    Task<OperationResult> Delete(Guid id);
    Task<OperationResult> AddChild(AddChildCourseCategoryCommand command);


    Task<List<CourseCategoryDto>> GetMainCategory();
    Task<CourseCategoryDto?> GetById(Guid id);
    Task<List<CourseCategoryDto>> GetChildrenCategory(Guid parentId);

}

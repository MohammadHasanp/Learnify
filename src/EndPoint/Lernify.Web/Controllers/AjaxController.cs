using Common.Application.FileUtil.StorageInterfaces;
using CoreModule.Application._Utilities;
using CoreModule.Facade.CourseCategories;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Controllers;

public class AjaxController(ICourseCategoryFacade categoryFacade, ILocalFileService localFileService) : Controller
{
    private readonly ICourseCategoryFacade _courseCategory = categoryFacade;
    private readonly ILocalFileService _localFileService = localFileService;

    [Route("/ajax/getCategoryChildren")]
    public async Task<IActionResult> GetCategoryChildren(Guid id)
    {
        var text = "";
        var childes = await _courseCategory.GetChildrenCategory(id);
        foreach (var item in childes)
        {
            text += $"<option value='{item.Id}'>{item.Title}</option>";
        }
        return new ObjectResult(text);
    }

    [Route("/Upload/ImageUploder")]
    public async Task<IActionResult> UploadImage(IFormFile upload)
    {
        var fileName = await _localFileService.SaveFileAndGenerateName(upload, CoreModuleDirectories.ImageUpload);
        var url = CoreModuleDirectories.GetImageUpload(fileName);
        return Json(new { uploaded = true, url });
    }
}
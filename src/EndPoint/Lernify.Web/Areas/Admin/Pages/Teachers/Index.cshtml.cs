using CoreModule.Facade.Teachers;
using CoreModule.Query.Teachers.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Areas.Admin.Pages.Teachers;

public class IndexModel(ITeacherFaced teacherFaced) : BaseRazorPage
{

    private readonly ITeacherFaced _teacherFaced = teacherFaced;

    public List<TeacherDto> Teacher { get; set; } = [];

    public async Task OnGet()
    {
        Teacher = await _teacherFaced.GetList();
    }

    public async Task<IActionResult> OnPostToggleStatus(Guid id)
    {
        return await AjaxTryCatch(() => _teacherFaced.ToggleStatus(id));
    }
}

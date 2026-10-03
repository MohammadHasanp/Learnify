using CoreModule.Application.Teachers.AcceptedRequest;
using CoreModule.Application.Teachers.RejectedRequest;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Teachers.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Areas.Admin.Pages.Teachers
{
    public class ShowModel(ITeacherFaced teacherFacade) : BaseRazorPage
    {
        private ITeacherFaced _teacherFacade = teacherFacade;

        public TeacherDto? Teacher { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            var teacher = await _teacherFacade.GetById(id);
            if (teacher == null)
                return RedirectToPage("Index");

            Teacher = teacher;
            return Page();
        }

        public async Task<IActionResult> OnPostAccept(Guid id)
        {
            return await AjaxTryCatch(() => _teacherFacade.Accepted(new AcceptedTeacherRequestCommand(id)));
        }
        public async Task<IActionResult> OnPostReject(Guid id, string description)
        {
            var result = await _teacherFacade.Rejected(new RejectedTeacherRequestCommand(id, description));
            return RedirectAndShowAlert(result, RedirectToPage("Index", new { id }));
        }
    }
}

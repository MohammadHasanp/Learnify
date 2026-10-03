using CoreModule.Application.Teachers.Register;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Facade.Teachers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.Util;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Profile
{
    [BindProperties]
    public class RegisterTeachersModel(ITeacherFaced faced) : BaseRazorPage
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; } = null!;

        [Display(Name = "رزومه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile CvFile { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var teacher = await faced.GetByUserId(User.GetUserId());
            if (teacher != null)
            {
                if (teacher.TeacherStatus == TeacherStatus.Active || teacher.TeacherStatus == TeacherStatus.Inactive)
                {
                    ErrorAlert("شما قبلا ثبت نام کرده اید ");
                }
                else
                {
                    SuccessAlert("درخواست شما در حال بررسی است ");
                }
                return RedirectToPage("Index");
            }
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            var result = await faced.Register(new RegisterTeacherCommand(CvFile, User.GetUserId(), UserName));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}

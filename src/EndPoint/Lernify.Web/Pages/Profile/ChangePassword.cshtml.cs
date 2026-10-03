using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.Util;
using UserModule.Core.Commands.Users.ChangePassword;
using UserModule.Core.Services;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Profile;

[BindProperties]
public class ChangePasswordModel(IUserService userService) : BaseRazorPage
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "{0} باید بیشتر از 6 کاراکتر باشد")]
    public string CurrentPassword { get; set; } = null!;

    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "{0} باید بیشتر از 6 کاراکتر باشد")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare("NewPassword", ErrorMessage = "تکرار کلمه عبور جدید نامعتبر است")]
    public string ConfirmPassword { get; set; } = null!;
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await userService.ChangePassword(new ChangeUserPasswordCommand(User.GetUserId(), CurrentPassword, NewPassword));
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

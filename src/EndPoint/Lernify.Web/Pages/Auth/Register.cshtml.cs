using Common.Application;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;

namespace Learnify.Web.Pages.Auth;

[BindProperties]
public class RegisterModel(IUserService service) : BaseRazorPage
{
    private readonly IUserService _service = service;

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Length(11, 11, ErrorMessage = "{0} را به درستی وارد کنید")]
    public string Mobile { get; set; } = null!;

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "{0} باید بیشتر از 6 کاراکتر باشد ")]
    public string Password { get; set; } = null!;

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password), ErrorMessage = "تکرار کلمه عبور صحیح نمی باشد")]
    public string ConfirmPassword { get; set; } = null!;

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _service.Register(new RegisterUserCommand(Password, Mobile));
        if (result.Status == EnOperationResultStatus.Success)
            result.Message = "ثبت نام با موفقیت انجام شد ";

        return RedirectAndShowAlert(result, RedirectToPage("Login"));
    }
}

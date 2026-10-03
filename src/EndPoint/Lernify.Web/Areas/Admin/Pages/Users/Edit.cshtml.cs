using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.RazorUtil;
using UserModule.Core.Commands.Users.FullEdit;
using UserModule.Core.Services;

namespace Learnify.Web.Areas.Admin.Pages.Users
{
    [BindProperties]
    public class EditModel(IUserService service) : BaseRazorPage
    {
        [Display(Name = "نام")]
        public string? Name { get; set; }
        [Display(Name = "نام خانوادگی")]

        public string? Family { get; set; }

        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; } = null!;


        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public List<Guid> CurrentUserRoles { get; set; } = [];


        public async Task<IActionResult> OnGet(Guid id)
        {
            var user = await service.GetUserById(id);
            if (user == null)
            {
                ErrorAlert("کاربر یافت نشد");
                return RedirectToPage("Index");
            }

            Name = user.Name;
            Family = user.Family;
            Email = user.Email;
            PhoneNumber = user.Mobile;
            CurrentUserRoles = user.Roles.Select(s => s.Id).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id, string[] roles)
        {
            var res = await service.FullEdit(new FullEditUserCommand
            {
                UserId = id,
                Name = Name,
                Family = Family,
                Email = Email,
                Mobile = PhoneNumber,
                Password = Password,
                Roles = roles.Select(Guid.Parse).ToList()
            });
            return RedirectAndShowAlert(res, RedirectToPage("Index"));
        }
    }
}

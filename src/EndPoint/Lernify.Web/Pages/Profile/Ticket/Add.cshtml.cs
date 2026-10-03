using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.Util;
using TicketModule.Services;
using TicketModule.Services.DTOs.Command;
using UserModule.Core.Services;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Profile.Ticket;

[BindProperties]
public class AddModel(IUserService userService, ITicketService ticketService) : BaseRazorPage
{
    [Display(Name = "عنوان تیکت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "متن تیکت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Text { get; set; } = null!;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await userService.GetUserByMobile(User.GetUserMobile());
        var ticket = new CreateTicketCommand()
        {
            Mobile = user!.Mobile,
            OwnerFullName = $"{user.Name} {user.Family}",
            Text = Text,
            Title = Title,
            UserId = User.GetUserId(),
        };
        var result = await ticketService.CreateTicket(ticket);
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

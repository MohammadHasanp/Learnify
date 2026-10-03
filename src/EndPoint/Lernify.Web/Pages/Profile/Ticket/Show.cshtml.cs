using Common.Application;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.Util;
using TicketModule.Services;
using TicketModule.Services.DTOs.Command;
using TicketModule.Services.DTOs.Query;
using UserModule.Core.Services;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Profile.Ticket;

public class ShowModel(ITicketService service, IUserService userService) : BaseRazorPage
{
    public TicketDto Ticket { get; set; } = new TicketDto();

    [BindProperty]
    [Display(Name = "متن پیام")]
    [Required(ErrorMessage = "{0}را وارد کنید ")]
    public string Text { get; set; } = null!;

    public async Task<IActionResult> OnGet(Guid id)
    {
        var ticket = await service.GetTicketById(id);

        if (ticket == null || ticket.UserId != User.GetUserId())
            return RedirectToPage("Index");

        Ticket = ticket;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid id)
    {
        var user = await userService.GetUserByMobile(User.GetUserMobile());
        var message = new SendTicketMessageCommand()
        {
            OwnerFullName = $"{user!.Name} {user.Family}",
            Text = Text,
            UserId = User.GetUserId(),
            TicketId = id,
        };

        var result = await service.SendMessageInTicket(message);
        return RedirectAndShowAlert(result, RedirectToPage("Show", new { id }));
    }

    public async Task<IActionResult> OnPostCloseTicket(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var ticket = await service.GetTicketById(id);
            if (ticket == null || ticket.UserId != User.GetUserId())
                return OperationResult.Error("تیکت یافت نشد");

            return await service.CloseTicket(id);
        });
    }
}
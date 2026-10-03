using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Common.Application;
using Learnify.Web.Infrastructure.Util;
using TicketModule.Services;
using TicketModule.Services.DTOs.Command;
using TicketModule.Services.DTOs.Query;
using UserModule.Core.Services;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Areas.Admin.Pages.Tickets
{
    public class ShowModel(ITicketService ticketService, IUserService userService) : BaseRazorPage
    {
        public TicketDto Ticket { get; set; } = null!;

        [BindProperty]
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        public string Text { get; set; } = null!;


        public async Task<IActionResult> OnGet(Guid ticketId)
        {
            var ticket = await ticketService.GetTicketById(ticketId);
            if (ticket == null)
                return RedirectToPage("Index");


            Ticket = ticket;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid ticketId)
        {
            var user = await userService.GetUserByMobile(User.GetUserMobile());
            var message = new SendTicketMessageCommand()
            {
                OwnerFullName = $"{user!.Name} {user.Family}",
                Text = Text,
                TicketId = ticketId,
                UserId = User.GetUserId()
            };
            var result = await ticketService.SendMessageInTicket(message);
            return RedirectAndShowAlert(result, RedirectToPage("Show", new { ticketId }));
        }


        public async Task<IActionResult> OnPostCloseTicket(Guid ticketId)
        {
            var ticket = await ticketService.GetTicketById(ticketId);
            return await AjaxTryCatch(async () =>
            {

                if (ticket == null || ticket.UserId != User.GetUserId())
                    return OperationResult.Error("تیکت یافت نشد");

                return await ticketService.CloseTicket(ticketId);
            });
        }
    }
}

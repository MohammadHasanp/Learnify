using AutoMapper;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using TicketModule.Context;
using TicketModule.Domain;
using TicketModule.Services.DTOs.Command;
using TicketModule.Services.DTOs.Query;

namespace TicketModule.Services;

public interface ITicketService
{
    public Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);
    public Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand messageCommand);
    public Task<OperationResult> CloseTicket(Guid ticketId);
    public Task<TicketDto?> GetTicketById(Guid id);
    public Task<TicketFilterResult> GetTicketByFilter(TicketFilterParams @params);
}


class TicketService(IMapper mapper, TicketContext context) : ITicketService
{
    public async Task<OperationResult> CloseTicket(Guid ticketId)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null)
            return OperationResult.NotFound();

        ticket.TicketStatus = TicketStatus.Closed;
        context.Tickets.Update(ticket);
        await context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command)
    {
        var ticket = mapper.Map<Ticket>(command);
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();
        return OperationResult<Guid>.Success(ticket.Id);
    }

    public async Task<TicketFilterResult> GetTicketByFilter(TicketFilterParams @params)
    {
        var result = context.Tickets.AsQueryable();

        if (@params.UserId != null)
            result = result.Where(t => t.UserId == @params.UserId);

        if (@params.Title != null)
            result = result.Where(s => s.Title.Contains(@params.Title));

        if (@params.TicketStatus != null!)
            result = result.Where(c => c.TicketStatus == @params.TicketStatus);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new TicketFilterResult()
        {
            Datas = await result.Skip(skip).Take(@params.Take).Select(t => new TicketFilterData()
            {
                CreationDate = t.CreationDate,
                Id = t.Id,
                IsDelete = t.IsDelete,
                TicketStatus = t.TicketStatus,
                Title = t.Title,
                UserId = t.UserId,
                OwnerFullName = t.OwnerFullName,
            }).ToListAsync()
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }


    public async Task<TicketDto?> GetTicketById(Guid id)
    {
        var ticket = await context.Tickets.Include(t => t.Messages).FirstOrDefaultAsync(t => t.Id == id);
        if (ticket == null)
            return null;

        return mapper.Map<TicketDto?>(ticket);
    }

    public async Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand messageCommand)
    {
        var ticket = await context.Tickets.FirstOrDefaultAsync(t => t.Id == messageCommand.TicketId);
        if (ticket == null)
            return OperationResult.NotFound();

        var message = new TicketMessage()
        {
            UserId = messageCommand.UserId,
            OwnerFullName = messageCommand.OwnerFullName,
            Text = messageCommand.Text,
            TicketId = ticket.Id,
        };

        if (ticket.UserId == messageCommand.UserId)
            ticket.TicketStatus = TicketStatus.Pending;
        else
            ticket.TicketStatus = TicketStatus.Answered;


        context.TicketMessages.Add(message);
        context.Tickets.Update(ticket);
        await context.SaveChangesAsync();
        return OperationResult.Success();
    }
}
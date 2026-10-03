using AutoMapper;
using TicketModule.Domain;
using TicketModule.Services.DTOs.Command;
using TicketModule.Services.DTOs.Query;

namespace TicketModule;

class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateTicketCommand, Ticket>().ReverseMap();
        CreateMap<SendTicketMessageCommand, Ticket>().ReverseMap();
        CreateMap<TicketDto, Ticket>().ReverseMap();
        CreateMap<TicketMessageDto, TicketMessage>().ReverseMap();
    }
}

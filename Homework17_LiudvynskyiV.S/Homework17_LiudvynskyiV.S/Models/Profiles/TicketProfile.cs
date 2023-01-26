using AutoMapper;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;

namespace Homework17_LiudvynskyiV.S.Models.Profiles;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketViewModel>().ReverseMap();
    }
}
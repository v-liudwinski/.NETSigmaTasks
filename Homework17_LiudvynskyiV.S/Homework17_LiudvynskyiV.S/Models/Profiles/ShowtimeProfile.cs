using AutoMapper;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;

namespace Homework17_LiudvynskyiV.S.Models.Profiles;

public class ShowtimeProfile : Profile
{
    public ShowtimeProfile()
    {
        CreateMap<Showtime, ShowtimeViewModel>().ReverseMap();
    }
}
using AutoMapper;
using Homework15_LiudvynskyiV.S.Models.Domain;
using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Models.Profiles;

public class ShowtimeProfile : Profile
{
    public ShowtimeProfile()
    {
        CreateMap<Showtime, ShowtimeViewModel>().ReverseMap();
    }
}
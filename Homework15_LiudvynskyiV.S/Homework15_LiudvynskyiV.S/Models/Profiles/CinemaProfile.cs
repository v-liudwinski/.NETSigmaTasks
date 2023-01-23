using AutoMapper;
using Homework15_LiudvynskyiV.S.Models.Domain;
using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Models.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<Cinema, CinemaViewModel>().ReverseMap();
    }
}
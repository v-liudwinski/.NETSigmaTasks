using AutoMapper;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;

namespace Homework17_LiudvynskyiV.S.Models.Profiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieViewModel>().ReverseMap();
    }
}
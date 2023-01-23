using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Repositories.Interfaces;

public interface ICinemaRepository
{
    Task<List<CinemaViewModel>> GetAll();
    Task<CinemaViewModel?> Get(Guid id);
    Task<CinemaViewModel?> Add(CinemaViewModel cinemaViewModel);
    Task<CinemaViewModel?> Update(Guid id, CinemaViewModel cinemaViewModel);
    Task<CinemaViewModel?> Delete(Guid id);
}
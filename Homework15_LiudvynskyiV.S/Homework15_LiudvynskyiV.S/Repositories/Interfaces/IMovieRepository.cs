using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<List<MovieViewModel>> GetAll();
    Task<MovieViewModel?> Get(Guid id);
    Task<MovieViewModel?> Add(MovieViewModel movieViewModel);
    Task<MovieViewModel?> Update(Guid id, MovieViewModel movieViewModel);
    Task<MovieViewModel?> Delete(Guid id);
}
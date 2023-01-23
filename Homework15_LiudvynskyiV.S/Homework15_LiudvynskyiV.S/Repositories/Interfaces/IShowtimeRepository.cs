using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Repositories.Interfaces;

public interface IShowtimeRepository
{
    Task<List<ShowtimeViewModel>> GetAll();
    Task<ShowtimeViewModel?> Get(Guid id);
    Task<ShowtimeViewModel?> Add(ShowtimeViewModel showtimeViewModel);
    Task<ShowtimeViewModel?> Update(Guid id, ShowtimeViewModel showtimeViewModel);
    Task<ShowtimeViewModel?> Delete(Guid id);
}
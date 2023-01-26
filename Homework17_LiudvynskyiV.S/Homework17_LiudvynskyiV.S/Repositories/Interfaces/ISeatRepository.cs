using Homework17_LiudvynskyiV.S.Models.ViewModels;

namespace Homework17_LiudvynskyiV.S.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<List<SeatViewModel>> GetAll();
    Task<SeatViewModel?> Get(Guid id);
    Task<SeatViewModel?> Add(SeatViewModel seatViewModel);
    Task<SeatViewModel?> Update(Guid id, SeatViewModel seatViewModel);
    Task<SeatViewModel?> Delete(Guid id);
}
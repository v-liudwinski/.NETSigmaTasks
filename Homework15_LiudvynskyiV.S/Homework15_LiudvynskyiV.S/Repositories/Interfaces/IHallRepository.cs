using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Repositories.Interfaces;

public interface IHallRepository
{
    Task<List<HallViewModel>> GetAll();
    Task<HallViewModel?> Get(Guid id);
    Task<HallViewModel?> Add(HallViewModel hallViewModel);
    Task<HallViewModel?> Update(Guid id, HallViewModel hallViewModel);
    Task<HallViewModel?> Delete(Guid id);
}
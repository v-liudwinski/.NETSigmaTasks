using Homework17_LiudvynskyiV.S.Models.ViewModels;

namespace Homework17_LiudvynskyiV.S.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserViewModel>> GetAll();
    Task<UserViewModel?> Get(Guid id);
    Task<UserViewModel?> Add(UserViewModel userViewModel);
    Task<UserViewModel?> Update(Guid id, UserViewModel userViewModel);
    Task<UserViewModel?> Delete(Guid id);
}
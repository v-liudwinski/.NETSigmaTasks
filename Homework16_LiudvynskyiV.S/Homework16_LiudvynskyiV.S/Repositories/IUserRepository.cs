using Homework16_LiudvynskyiV.S.Models.Domain;
using Homework16_LiudvynskyiV.S.Models.DTO;

namespace Homework16_LiudvynskyiV.S.Repositories;

public interface IUserRepository
{
    User CreateUser(CreateUserDTO userDto);
}
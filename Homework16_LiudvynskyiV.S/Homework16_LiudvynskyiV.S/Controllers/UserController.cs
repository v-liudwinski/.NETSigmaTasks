using Homework16_LiudvynskyiV.S.Models.Domain;
using Homework16_LiudvynskyiV.S.Models.DTO;
using Homework16_LiudvynskyiV.S.Validators;

namespace Homework16_LiudvynskyiV.S.Controllers;

public class UserController
{
    private readonly IPasswordValidator _passwordValidator;

    public UserController(IPasswordValidator passwordValidator)
    {
        _passwordValidator = passwordValidator;
    }

    public User CreateUser(CreateUserDTO userDto)
    {
        throw new NotImplementedException();
    }
    
    // Validation
    public bool ValidateUserDto(CreateUserDTO userDto, out string message)
    {
        var isNameValid = userDto.Name.Length >= 3;
        if (!isNameValid)
        {
            message = "Invalid name!";
            return false;
        }
        
        var isPasswordValid = _passwordValidator.IsValid(userDto.Password);
        if (!isPasswordValid)
        {
            message = "Invalid password!";
            return false;
        }
        message = "Success!";
        return true;
    }
}
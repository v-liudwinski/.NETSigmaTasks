using Homework16_LiudvynskyiV.S.Controllers;
using Homework16_LiudvynskyiV.S.Models.Domain;
using Homework16_LiudvynskyiV.S.Models.DTO;
using Homework16_LiudvynskyiV.S.Validators;
using Moq;
using ServicesTetst.Stubs;

namespace ServicesTetst;

public class UserTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ValidateUser_CorrectValidation_Success()
    {
        // Arrange
        var name = "Jhon";
        var password = "abcd123";
        var expected = true;
        var expectedMessage = "Success!";
        
        var mock = new Mock<IPasswordValidator>();
        mock.Setup(x => x.IsValid(password)).Returns(true);
        var userController = new UserController(mock.Object);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password
        };
        
        // Act
        var result = userController.ValidateUserDto(user, out string message);

        // Assert
        Assert.That(expected, Is.EqualTo(result));
        Assert.That(expectedMessage, Is.EqualTo(message));
    }

    [Test]
    public void ValidateUser_InvalidName_CorrectMessage()
    {
        // Arrange
        var name = "";
        var password = "abcd123";
        var expected = false;
        var expectedMessage = "Invalid name!";
        
        var mock = new Mock<IPasswordValidator>();
        mock.Setup(x => x.IsValid(password)).Returns(true);
        var userController = new UserController(mock.Object);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password
        };
        
        // Act
        var result = userController.ValidateUserDto(user, out string message);

        // Assert
        Assert.That(expected, Is.EqualTo(result));
        Assert.That(expectedMessage, Is.EqualTo(message));
    }

    [Test]
    public void ValidateUser_InvalidPassword_CorrectMessage()
    {
        // Arrange
        var name = "Jhon";
        var password = "";
        var expected = false;
        var expectedMessage = "Invalid password!";
        
        var mock = new Mock<IPasswordValidator>();
        mock.Setup(x => x.IsValid(password)).Returns(false);
        var userController = new UserController(mock.Object);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password
        };
        
        // Act
        var result = userController.ValidateUserDto(user, out string message);

        // Assert
        Assert.That(expected, Is.EqualTo(result));
        Assert.That(expectedMessage, Is.EqualTo(message));
    }

    [Test]
    public void ValidateUser_InvalidName_PasswordValidationWasNeverCalled()
    {
        // Arrange
        var name = "";
        var password = "asfsadfdasf";
        var user = new CreateUserDTO
        {
            Name = name,
            Password = password
        };
        var mock = new Mock<IPasswordValidator>();
        var userController = new UserController(mock.Object);

        // Act
        var result = userController.ValidateUserDto(user, out var maessage);

        // Assert
        mock.Verify(x => x.IsValid(password), Times.Never);
    }
}
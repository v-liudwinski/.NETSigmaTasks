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
        var email = "jhon.smith@gmail.com";
        var expected = true;
        var expectedMessage = "Success!";
        
        var passwordMock = new Mock<IPasswordValidator>();
        var emailValidator = new EmailValidator();
        passwordMock.Setup(x => x.IsValid(password)).Returns(true);
        var userController = new UserController(passwordMock.Object, emailValidator);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password,
            Email = email
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
        var email = "jhon.smith@gmail.com";
        var expected = false;
        var expectedMessage = "Invalid name!";
        
        var mock = new Mock<IPasswordValidator>();
        var emailValidator = new EmailValidator();
        mock.Setup(x => x.IsValid(password)).Returns(true);
        var userController = new UserController(mock.Object, emailValidator);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password,
            Email = email
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
        var email = "jhon.smith@gmail.com";
        var expected = false;
        var expectedMessage = "Invalid password!";
        
        var mock = new Mock<IPasswordValidator>();
        var emailValidator = new EmailValidator();
        mock.Setup(x => x.IsValid(password)).Returns(false);
        var userController = new UserController(mock.Object, emailValidator);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password,
            Email = email
        };
        
        // Act
        var result = userController.ValidateUserDto(user, out string message);

        // Assert
        Assert.That(expected, Is.EqualTo(result));
        Assert.That(expectedMessage, Is.EqualTo(message));
    }

    [Test]
    [TestCase("Jhon", "abcd123", "ssfdsfdssaad", false, "Invalid email!")]
    [TestCase("Jhon", "abcd123", "@", false, "Invalid email!")]
    [TestCase("Jhon", "abcd123", "@gmail.com", false, "Invalid email!")]
    [TestCase("Jhon", "abcd123", "gmail.com", false, "Invalid email!")]
    public void ValidateUser_InvalidEmail_CorrectMessage
        (string name, string password, string email, bool expected, string expectedMessage)
    {
        // Arrange
        var mock = new Mock<IPasswordValidator>();
        var emailValidator = new EmailValidator();
        mock.Setup(x => x.IsValid(password)).Returns(true);
        var userController = new UserController(mock.Object, emailValidator);
        var user = new CreateUserDTO()
        {
            Name = name,
            Password = password,
            Email = email
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
        var email = "jhon.smith@gmail.com";
        var password = "asfsadfdasf";
        var user = new CreateUserDTO
        {
            Name = name,
            Password = password,
            Email = email
        };
        var mock = new Mock<IPasswordValidator>();
        var emailValidator = new EmailValidator();
        var userController = new UserController(mock.Object, emailValidator);

        // Act
        var result = userController.ValidateUserDto(user, out var maessage);

        // Assert
        mock.Verify(x => x.IsValid(password), Times.Never);
    }
}
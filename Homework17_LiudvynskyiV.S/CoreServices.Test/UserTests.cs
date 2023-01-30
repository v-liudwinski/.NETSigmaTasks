using AutoMapper;
using EntityFrameworkCoreMock;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.Profiles;
using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories;
using Homework17_LiudvynskyiV.S.Validators;
using Microsoft.EntityFrameworkCore;

namespace CoreServices.Test;

public class UserTests
{
    private List<User> users;
    private IMapper _mapper;
    private DbContextMock<CinemaNetworkDbContext> _contextMock;
    
    [SetUp]
    public void Setup()
    {
        var userProfile = new UserProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(userProfile));
        _mapper = new Mapper(configuration);

        users = new List<User>
        {
            new() { Id = Guid.Parse("da613008-4116-4629-8892-5626f58ca577"), Name = "Jhonny", Surname = "Depp", 
                Email = "jd@gmail.com" },
            new() { Id = Guid.Parse("972e740e-d451-497f-9653-f769687181b3"), Name = "Leonardo", Surname = "Di Caprio",
                Email = "ld@gmail.com" },
            new() { Id = Guid.Parse("a336df90-3f56-4dfc-8c10-98c4e5bd6da2"), Name = "Brad", Surname = "Pitt", 
                Email = "bp@gmail.com" },
            new() { Id = Guid.Parse("bb777e45-3209-4460-bb95-523d3fdfe39f"), Name = "Charlie", Surname = "Hunnam", 
                Email = "ch@gmail.com" },
            new() { Id = Guid.Parse("d22bb8f2-c9a4-4ffe-aedc-e14b14a6a80c"), Name = "Jackie", Surname = "Chan", 
                Email = "jc@gmail.com" }
        };

        _contextMock = new DbContextMock<CinemaNetworkDbContext>
            (new DbContextOptionsBuilder<CinemaNetworkDbContext>().Options);
        _contextMock.CreateDbSetMock(x => x.Users, users);
    }

    [Test]
    [TestCase(5)]
    public async Task GetAllAsync_ReadingAllUsersFromDb_Success(int expectedResult)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);

        // Act
        var result = await repository.GetAll();

        // Assert
        Assert.That(result, Has.Count.EqualTo(expectedResult));
    }
    
    [Test]
    [TestCase("da613008-4116-4629-8892-5626f58ca577", "Jhonny", "Depp", "jd@gmail.com")]
    [TestCase("972e740e-d451-497f-9653-f769687181b3", "Leonardo", "Di Caprio", "ld@gmail.com")]
    [TestCase("a336df90-3f56-4dfc-8c10-98c4e5bd6da2", "Brad", "Pitt", "bp@gmail.com")]
    public async Task GetAsync_ReadingUserFromDb_Success
        (string id, string expectedName, string expectedSurname, string expectedEmail)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);

        // Act
        var result = await repository.Get(Guid.Parse(id));

        // Assert
        Assert.That(result.Name, Is.EqualTo(expectedName));
        Assert.That(result.Surname, Is.EqualTo(expectedSurname));
        Assert.That(result.Email, Is.EqualTo(expectedEmail));
    }

    [Test]
    [TestCase("Elon", "Mask", "em@gmail.com")]
    public async Task AddAsync_AddingNewUserToDb_Success(string name, string surname, string email)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);
        var userVM = new UserViewModel
        {
            Name = name,
            Surname = surname,
            Email = email
        };

        // Act
        var result = await repository.Add(userVM);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(name));
        Assert.That(result.Surname, Is.EqualTo(surname));
        Assert.That(result.Email, Is.EqualTo(email));
    }
    
    [Test]
    [TestCase("Elon", "Mask", "@gmail.com")]
    [TestCase("Elon", "Mask", "dfdsfdsfd")]
    [TestCase("Elon", "Mask", "gmail.com@")]
    [TestCase("Elon", "Mask", "gmail.com")]
    public async Task AddAsync_AddingNewUserToDb_NullBecauseOfInvalidEmail(string name, string surname, string email)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);
        var userVM = new UserViewModel
        {
            Name = name,
            Surname = surname,
            Email = email
        };

        // Act
        var result = await repository.Add(userVM);

        // Assert
        Assert.Null(result);
    }
    
    [Test]
    [TestCase("da613008-4116-4629-8892-5626f58ca577", "Elon", "Mask", "em@gmail.com")]
    public async Task UpdateAsync_UpdatingUserInDb_Success(string id, string name, string surname, string email)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);
        var userVM = new UserViewModel
        {
            Name = name,
            Surname = surname,
            Email = email
        };

        // Act
        var previousModelData = await repository.Get(Guid.Parse(id));
        var result = await repository.Update(Guid.Parse(id), userVM);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Name, Is.EqualTo(name));
        Assert.That(result.Surname, Is.EqualTo(surname));
        Assert.That(result.Email, Is.EqualTo(email));
        Assert.That(previousModelData.Name, Is.Not.EqualTo(result.Name));
        Assert.That(previousModelData.Surname, Is.Not.EqualTo(result.Surname));
        Assert.That(previousModelData.Email, Is.Not.EqualTo(result.Email));
    }
    
    [Test]
    [TestCase("da613008-4116-4629-8892-5626f58ca577", "Elon", "Mask", "@gmail.com")]
    [TestCase("da613008-4116-4629-8892-5626f58ca577","Elon", "Mask", "dfdsfdsfd")]
    [TestCase("da613008-4116-4629-8892-5626f58ca577", "Elon", "Mask", "gmail.com@")]
    [TestCase("da613008-4116-4629-8892-5626f58ca577", "Elon", "Mask", "gmail.com")]
    public async Task UpdateAsync_UpdatingUserInDb_NullBecauseOfInvalidEmail
        (string id, string name, string surname, string email)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);
        var userVM = new UserViewModel
        {
            Name = name,
            Surname = surname,
            Email = email
        };

        // Act
        var result = await repository.Update(Guid.Parse(id), userVM);

        // Assert
        Assert.Null(result);
    }

    [Test]
    [TestCase("da613008-4116-4629-8892-5626f58ca577")]
    public async Task DeleteAsync_DeletingUserFromDb_Success(string id)
    {
        // Arrange
        var validator = new EmailValidator();
        var repository = new UserRepository(_contextMock.Object, _mapper, validator);
        
        // Act
        var result = await repository.Delete(Guid.Parse(id));
        var tryGetModelAfterDeleting = await repository.Get(Guid.Parse(id));

        // Assert
        Assert.NotNull(result);
        Assert.Null(tryGetModelAfterDeleting);
    }
}
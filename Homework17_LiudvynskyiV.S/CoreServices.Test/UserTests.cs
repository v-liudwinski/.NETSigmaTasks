using AutoMapper;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.Profiles;
using Homework17_LiudvynskyiV.S.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreServices.Test;

public class UserTests
{
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        var userProfile = new UserProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(userProfile));
        _mapper = new Mapper(configuration);
    }

    [Test]
    public void GetAllTest()
    {
        Setup();
        var options = new DbContextOptionsBuilder<CinemaNetworkDbContext>()
            .UseInMemoryDatabase(databaseName: "MoviesNetworkDb")
            .Options;

        using (var context = new CinemaNetworkDbContext(options))
        {
            context.Users.AddRange(
                new User { Id = Guid.Parse("da613008-4116-4629-8892-5626f58ca577"), Name = "Jhonny", Surname = "Depp", Email = "123@gmail.com"},
                new User { Id = Guid.Parse("972e740e-d451-497f-9653-f769687181b3"), Name = "Leonardo", Surname = "Di Caprio", Email = "555@gmail.com" },
                new User { Id = Guid.Parse("a336df90-3f56-4dfc-8c10-98c4e5bd6da2"), Name = "Brad", Surname = "Pitt", Email = "333@gmail.com" },
                new User { Id = Guid.Parse("bb777e45-3209-4460-bb95-523d3fdfe39f"), Name = "Charlie", Surname = "Hunnam", Email = "222@gmail.com" },
                new User { Id = Guid.Parse("d22bb8f2-c9a4-4ffe-aedc-e14b14a6a80c"), Name = "Jackie", Surname = "Chan", Email = "111@gmail.com" }
            );
            context.SaveChanges();
        }
        
        using (var context = new CinemaNetworkDbContext(options))
        {
            var userRepository = new UserRepository(context, _mapper);
            var users = userRepository.GetAll();
            Assert.That(users.Result, Has.Count.EqualTo(5));
        }
    }
}
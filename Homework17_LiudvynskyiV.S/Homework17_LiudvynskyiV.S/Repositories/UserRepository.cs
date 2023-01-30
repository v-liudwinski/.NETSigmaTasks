using AutoMapper;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Homework17_LiudvynskyiV.S.Validators;
using Microsoft.EntityFrameworkCore;

namespace Homework17_LiudvynskyiV.S.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IUserEmailValidator _validator;
    private readonly IMapper _mapper;

    public UserRepository(CinemaNetworkDbContext dbContext, IMapper mapper, IUserEmailValidator validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<List<UserViewModel>> GetAll()
    {
        var users = await _dbContext.Users.ToListAsync();
        return _mapper.Map<List<UserViewModel>>(users);
    }

    public async Task<UserViewModel?> Get(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user is null ? default : _mapper.Map<UserViewModel>(user);
    }

    public async Task<UserViewModel?> Add(UserViewModel userViewModel)
    {
        if (userViewModel is null) return default;
        if (!_validator.IsValidEmail(userViewModel.Email)) return default;
        
        var user = _mapper.Map<User>(userViewModel);
        user.Id = new Guid();
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return userViewModel;
    }

    public async Task<UserViewModel?> Update(Guid id, UserViewModel userViewModel)
    {
        if (userViewModel is null) return default;
        if (!_validator.IsValidEmail(userViewModel.Email)) return default;
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null) return default;
        user.Name = userViewModel.Name;
        user.Surname = userViewModel.Surname;
        user.Email = userViewModel.Email;
        await _dbContext.SaveChangesAsync();
        return userViewModel;
    }

    public async Task<UserViewModel?> Delete(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null) return default;
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<UserViewModel>(user);
    }
}
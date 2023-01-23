using AutoMapper;
using Homework15_LiudvynskyiV.S.Data;
using Homework15_LiudvynskyiV.S.Models.Domain;
using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Homework15_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework15_LiudvynskyiV.S.Repositories;

public class HallRepository : IHallRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IMapper _mapper;

    public HallRepository(CinemaNetworkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<HallViewModel>> GetAll()
    {
        var halls = await _dbContext.Halls
            .Include(x => x.Cinema)
            .ToListAsync();
        return _mapper.Map<List<HallViewModel>>(halls);
    }

    public async Task<HallViewModel?> Get(Guid id)
    {
        var hall = await _dbContext.Halls
            .Include(x => x.Cinema)
            .FirstOrDefaultAsync(x => x.Id == id);
        return hall is null ? default : _mapper.Map<HallViewModel>(hall);
    }

    public async Task<HallViewModel?> Add(HallViewModel hallViewModel)
    {
        if (hallViewModel is null) return default;
        var hall = _mapper.Map<Hall>(hallViewModel);
        hall.Id = new Guid();
        await _dbContext.Halls.AddAsync(hall);
        await _dbContext.SaveChangesAsync();
        return hallViewModel;
    }

    public async Task<HallViewModel?> Update(Guid id, HallViewModel hallViewModel)
    {
        if (hallViewModel is null) return default;
        var hall = await _dbContext.Halls.FirstOrDefaultAsync(x => x.Id == id);
        if (hall is null) return default;
        _mapper.Map<Hall>(hallViewModel);
        await _dbContext.SaveChangesAsync();
        return hallViewModel;
    }

    public async Task<HallViewModel?> Delete(Guid id)
    {
        var hall = await _dbContext.Halls.FirstOrDefaultAsync(x => x.Id == id);
        if (hall is null) return default;
        _dbContext.Halls.Remove(hall);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<HallViewModel>(hall);
    }
}
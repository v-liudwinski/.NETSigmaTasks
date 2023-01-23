using AutoMapper;
using Homework15_LiudvynskyiV.S.Data;
using Homework15_LiudvynskyiV.S.Models.Domain;
using Homework15_LiudvynskyiV.S.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Homework15_LiudvynskyiV.S.Repositories;

public class CinemaRepository : Interfaces.ICinemaRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IMapper _mapper;

    public CinemaRepository(CinemaNetworkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<CinemaViewModel>> GetAll()
    {
        var cinemas = await _dbContext.Cinemas.ToListAsync();
        return _mapper.Map<List<CinemaViewModel>>(cinemas);
    }

    public async Task<CinemaViewModel?> Get(Guid id)
    {
        var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
        return cinema is null ? default : _mapper.Map<CinemaViewModel>(cinema);
    }

    public async Task<CinemaViewModel?> Add(CinemaViewModel cinemaViewModel)
    {
        if (cinemaViewModel is null) return default;
        var cinema = _mapper.Map<Cinema>(cinemaViewModel);
        cinema.Id = new Guid();
        await _dbContext.Cinemas.AddAsync(cinema);
        await _dbContext.SaveChangesAsync();
        return cinemaViewModel;
    }

    public async Task<CinemaViewModel?> Update(Guid id, CinemaViewModel cinemaViewModel)
    {
        if (cinemaViewModel is null) return default;
        var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
        if (cinema is null) return default;
        _mapper.Map<Cinema>(cinemaViewModel);
        await _dbContext.SaveChangesAsync();
        return cinemaViewModel;
    }

    public async Task<CinemaViewModel?> Delete(Guid id)
    {
        var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
        if (cinema is null) return default;
        _dbContext.Cinemas.Remove(cinema);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<CinemaViewModel>(cinema);
    }
}
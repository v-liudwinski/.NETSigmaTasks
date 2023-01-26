using AutoMapper;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework17_LiudvynskyiV.S.Repositories;

public class ShowtimeRepository : IShowtimeRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IMapper _mapper;

    public ShowtimeRepository(CinemaNetworkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<ShowtimeViewModel>> GetAll()
    {
        var showtimes = await _dbContext.Showtimes.ToListAsync();
        return _mapper.Map<List<ShowtimeViewModel>>(showtimes);
    }

    public async Task<ShowtimeViewModel?> Get(Guid id)
    {
        var showtime = await _dbContext.Showtimes.FirstOrDefaultAsync(x => x.Id == id);
        return showtime is null ? default : _mapper.Map<ShowtimeViewModel>(showtime);
    }

    public async Task<ShowtimeViewModel?> Add(ShowtimeViewModel showtimeViewModel)
    {
        if (showtimeViewModel is null) return default;
        var showtime = _mapper.Map<Showtime>(showtimeViewModel);
        showtime.Id = new Guid();
        await _dbContext.Showtimes.AddAsync(showtime);
        await _dbContext.SaveChangesAsync();
        return showtimeViewModel;
    }

    public async Task<ShowtimeViewModel?> Update(Guid id, ShowtimeViewModel showtimeViewModel)
    {
        if (showtimeViewModel is null) return default;
        var showtime = await _dbContext.Showtimes.FirstOrDefaultAsync(x => x.Id == id);
        if (showtime is null) return default;
        _mapper.Map<Showtime>(showtimeViewModel);
        await _dbContext.SaveChangesAsync();
        return showtimeViewModel;
    }

    public async Task<ShowtimeViewModel?> Delete(Guid id)
    {
        var showtime = await _dbContext.Showtimes.FirstOrDefaultAsync(x => x.Id == id);
        if (showtime is null) return default;
        _dbContext.Showtimes.Remove(showtime);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<ShowtimeViewModel>(showtime);
    }
}
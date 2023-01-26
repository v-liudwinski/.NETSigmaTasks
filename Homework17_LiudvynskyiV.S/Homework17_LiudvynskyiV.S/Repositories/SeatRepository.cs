using AutoMapper;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework17_LiudvynskyiV.S.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IMapper _mapper;

    public SeatRepository(CinemaNetworkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<SeatViewModel>> GetAll()
    {
        var seats = await _dbContext.Seats
            .Include(x => x.Hall)
            .ToListAsync();
        return _mapper.Map<List<SeatViewModel>>(seats);
    }

    public async Task<SeatViewModel?> Get(Guid id)
    {
        var seat = await _dbContext.Seats
            .Include(x => x.Hall)
            .FirstOrDefaultAsync(x => x.Id == id);
        return seat is null ? default : _mapper.Map<SeatViewModel>(seat);
    }

    public async Task<SeatViewModel?> Add(SeatViewModel seatViewModel)
    {
        if (seatViewModel is null) return default;
        var seat = _mapper.Map<Seat>(seatViewModel);
        seat.Id = new Guid();
        await _dbContext.Seats.AddAsync(seat);
        await _dbContext.SaveChangesAsync();
        return seatViewModel;
    }

    public async Task<SeatViewModel?> Update(Guid id, SeatViewModel seatViewModel)
    {
        if (seatViewModel is null) return default;
        var movie = await _dbContext.Seats.FirstOrDefaultAsync(x => x.Id == id);
        if (movie is null) return default;
        _mapper.Map<Movie>(seatViewModel);
        await _dbContext.SaveChangesAsync();
        return seatViewModel;
    }

    public async Task<SeatViewModel?> Delete(Guid id)
    {
        var seat = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (seat is null) return default;
        _dbContext.Movies.Remove(seat);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<SeatViewModel>(seat);
    }
}
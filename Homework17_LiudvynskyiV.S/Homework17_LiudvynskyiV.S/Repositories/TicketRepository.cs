using AutoMapper;
using Homework17_LiudvynskyiV.S.Data;
using Homework17_LiudvynskyiV.S.Models.Domain;
using Homework17_LiudvynskyiV.S.Models.ViewModels;
using Homework17_LiudvynskyiV.S.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homework17_LiudvynskyiV.S.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CinemaNetworkDbContext _dbContext;
    private readonly IMapper _mapper;

    public TicketRepository(CinemaNetworkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<TicketViewModel>> GetAll()
    {
        var tickets = await _dbContext.Tickets
            .Include(x => x.Movie)
            .Include(x => x.Seat)
            .Include(x => x.Showtime)
            .Include(x => x.User)
            .ToListAsync();
        return _mapper.Map<List<TicketViewModel>>(tickets);
    }

    public async Task<TicketViewModel?> Get(Guid id)
    {
        var ticket = await _dbContext.Tickets
            .Include(x => x.Movie)
            .Include(x => x.Seat)
            .Include(x => x.Showtime)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
        return ticket is null ? default : _mapper.Map<TicketViewModel>(ticket);
    }

    public async Task<TicketViewModel?> Add(TicketViewModel ticketViewModel)
    {
        if (ticketViewModel is null) return default;
        var ticket = _mapper.Map<Ticket>(ticketViewModel);
        ticket.Id = new Guid();
        await _dbContext.Tickets.AddAsync(ticket);
        await _dbContext.SaveChangesAsync();
        return ticketViewModel;
    }

    public async Task<TicketViewModel?> Update(Guid id, TicketViewModel ticketViewModel)
    {
        if (ticketViewModel is null) return default;
        var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        if (ticket is null) return default;
        _mapper.Map<Ticket>(ticketViewModel);
        await _dbContext.SaveChangesAsync();
        return ticketViewModel;
    }

    public async Task<TicketViewModel?> Delete(Guid id)
    {
        var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        if (ticket is null) return default;
        _dbContext.Tickets.Remove(ticket);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TicketViewModel>(ticket);
    }
}
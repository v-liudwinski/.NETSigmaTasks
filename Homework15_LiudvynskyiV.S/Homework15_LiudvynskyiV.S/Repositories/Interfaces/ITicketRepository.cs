using Homework15_LiudvynskyiV.S.Models.ViewModels;

namespace Homework15_LiudvynskyiV.S.Repositories.Interfaces;

public interface ITicketRepository
{
    Task<List<TicketViewModel>> GetAll();
    Task<TicketViewModel?> Get(Guid id);
    Task<TicketViewModel?> Add(TicketViewModel ticketViewModel);
    Task<TicketViewModel?> Update(Guid id, TicketViewModel ticketViewModel);
    Task<TicketViewModel?> Delete(Guid id);
}
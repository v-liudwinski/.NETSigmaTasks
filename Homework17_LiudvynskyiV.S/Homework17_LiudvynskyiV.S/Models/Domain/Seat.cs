namespace Homework17_LiudvynskyiV.S.Models.Domain;

public class Seat : IEntity
{
    public Guid Id { get; set; }
    public int SeatNumber { get; set; }
    public Guid HallId { get; set; }
    // Navigation property
    public Hall Hall { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}
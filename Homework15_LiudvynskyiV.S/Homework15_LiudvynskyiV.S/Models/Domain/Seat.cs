namespace Homework15_LiudvynskyiV.S.Models.Domain;

public class Seat
{
    public Guid Id { get; set; }
    public int SeatNumber { get; set; }
    public Guid HallId { get; set; }
    // Navigation property
    public Hall Hall { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}
namespace Homework15_LiudvynskyiV.S.Models.Domain;

public class Showtime
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    // Navigation property
    public ICollection<Ticket> Tickets { get; set; }
}
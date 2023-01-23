namespace Homework15_LiudvynskyiV.S.Models.Domain;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SeatId { get; set; }
    public Guid ShowtimeId { get; set; }
    public Guid MovieId { get; set; }
    public decimal Price { get; set; }
    // Navigation property
    public User? User { get; set; }
    public Seat Seat { get; set; }
    public Showtime Showtime { get; set; }
    public Movie Movie { get; set; }
}
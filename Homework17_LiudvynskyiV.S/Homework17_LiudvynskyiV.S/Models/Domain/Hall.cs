namespace Homework17_LiudvynskyiV.S.Models.Domain;

public class Hall : IEntity
{
    public Guid Id { get; set; }
    public int HallNumber { get; set; }
    public Guid CinemaId { get; set; }
    // Navigation property
    public Cinema Cinema { get; set; }
    public ICollection<Seat> Seats { get; set; }
}
namespace Homework15_LiudvynskyiV.S.Models.Domain;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    // Navigation property
    public ICollection<Ticket> Tickets { get; set; }
}
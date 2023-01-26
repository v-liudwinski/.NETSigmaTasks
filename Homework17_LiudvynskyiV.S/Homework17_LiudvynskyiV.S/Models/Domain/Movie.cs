namespace Homework17_LiudvynskyiV.S.Models.Domain;

public class Movie : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    // Navigation property
    public ICollection<Ticket> Tickets { get; set; }
}
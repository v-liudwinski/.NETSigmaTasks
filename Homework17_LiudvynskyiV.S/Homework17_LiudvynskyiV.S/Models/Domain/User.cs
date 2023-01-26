namespace Homework17_LiudvynskyiV.S.Models.Domain;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    // Navigation property
    public ICollection<Ticket> Tickets { get; set; }
}
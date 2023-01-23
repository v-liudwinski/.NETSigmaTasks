namespace Homework15_LiudvynskyiV.S.Models.Domain;

public class Cinema
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    // Navigation property
    public ICollection<Hall> Halls { get; set; }
}
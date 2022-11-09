namespace Homework6_LiudvynskyiV.S.Models;

public class InmateInformation
{
    public int FlatNumber { get; set; }
    public string Street { get; set; }
    public string InmateSurname { get; set; }
    public IEnumerable<CounterCheck> CounterChecks { get; set; }
}
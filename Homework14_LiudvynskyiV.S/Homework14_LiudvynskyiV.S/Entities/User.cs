namespace Homework14_LiudvynskyiV.S.Entities;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public override string ToString()
    {
        return $"Id: {UserId} | Name: {FirstName} | Surname: {LastName}";
    }
}
using Homework14_LiudvynskyiV.S;

const string connectionString = @"Server=localhost;Port=5432;Database=CinemaNetwork;User Id=postgres;Password=admin;";

var userData = new UserData(connectionString);
var users = userData.GetUsers();
foreach (var user in users)
{
    Console.WriteLine(user.ToString());
}
Console.WriteLine(userData.GetUserById(1));
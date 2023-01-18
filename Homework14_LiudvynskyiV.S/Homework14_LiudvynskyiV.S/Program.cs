using Homework14_LiudvynskyiV.S;
using Homework14_LiudvynskyiV.S.Entities;

const string connectionString = @"Server=pglocalhost;Port=5432;Database=CinemaNetwork;User Id=postgres;Password=admin;";

var userData = new UserData(connectionString);
var users = userData.GetUsers();
foreach (var user in users)
{
    Console.WriteLine(user.ToString());
}
Console.WriteLine(userData.GetUserById(1));
Console.WriteLine(userData.SaveUser(new User
{
    FirstName = "Charlie",
    LastName = "Chaplin"
}));
Console.WriteLine(userData.InsertUser(new User
{
    FirstName = "David",
    LastName = "Backham"
}));
Console.WriteLine(userData.DeleteUser(2));
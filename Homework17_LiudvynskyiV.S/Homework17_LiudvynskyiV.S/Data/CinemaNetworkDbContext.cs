using Homework17_LiudvynskyiV.S.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Homework17_LiudvynskyiV.S.Data;

public class CinemaNetworkDbContext : DbContext
{
    public CinemaNetworkDbContext(DbContextOptions<CinemaNetworkDbContext> options) : base(options) { }

    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Showtime> Showtimes { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
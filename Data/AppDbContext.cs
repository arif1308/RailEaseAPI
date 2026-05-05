using Microsoft.EntityFrameworkCore;
using RailEaseAPI.Models;

namespace RailEaseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainCategory> TrainCategories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
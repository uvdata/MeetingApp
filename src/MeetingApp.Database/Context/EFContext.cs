using Microsoft.EntityFrameworkCore;
using MeetingApp.Domain;

namespace MeetingApp.Database.Context
{
    public sealed class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MeetingRoom>().ToTable("MeetingRoom");
            builder.Entity<Location>().ToTable("Location");
        }
    }
}

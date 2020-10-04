
using Microsoft.EntityFrameworkCore;
using aspreact.Models;
using aspreact.Maps;
using System;

namespace aspreact.Models{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<MeetingRoomModel> MeetingRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            base.OnModelCreating(modelBuilder);

            new MeetingRoomMap(modelBuilder.Entity<MeetingRoomModel>());
        }
    }
}
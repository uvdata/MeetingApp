using aspreact.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;

namespace aspreact.Maps{
    public class MeetingRoomMap{
        public MeetingRoomMap(EntityTypeBuilder<MeetingRoomModel> entityBuilder){
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("meeting_room");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Name).HasColumnName("name");
            entityBuilder.Property(x => x.Location).HasColumnName("location");
            entityBuilder.Property(x => x.Description).HasColumnName("description");
            entityBuilder.Property(x => x.TotalSeats).HasColumnName("total_seats");
        }
    }
}
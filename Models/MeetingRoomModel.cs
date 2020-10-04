using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace aspreact.Models{
    public class MeetingRoomModel{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName="geometry (point)")]
        public Point Location { get; set; }
        public int TotalSeats { get; set; }
    }
}
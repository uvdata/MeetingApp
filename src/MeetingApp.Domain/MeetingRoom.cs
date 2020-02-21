using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingApp.Domain
{
    public class MeetingRoom : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int Seats { get; set; }
    }
}

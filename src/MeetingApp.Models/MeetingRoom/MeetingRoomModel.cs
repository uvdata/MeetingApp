using MeetingApp.Models.Location;

namespace MeetingApp.Models.MeetingRoom
{
    public class MeetingRoomModel : IMeetingRoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public LocationModel Location { get; set; }
    }
}

using MeetingApp.Models.Location;

namespace MeetingApp.Models.MeetingRoom
{
    public interface IMeetingRoomModel
    {
        int Id { get; set; }
        string Name { get; set; }
        int Seats { get; set; }
        LocationModel Location { get; set; }
    }
}

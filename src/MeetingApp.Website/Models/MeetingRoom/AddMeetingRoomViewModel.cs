using System.Collections.Generic;
using MeetingApp.Models.Location;
using MeetingApp.Models.MeetingRoom;

namespace MeetingApp.Website.Models.MeetingRoom
{
    public class AddMeetingRoomViewModel
    {
        public AddMeetingRoomModel MeetingRoom { get; set; }
        public List<LocationModel> LocationModels { get; set; }
        public int LocationId { get; set; }
    }
}

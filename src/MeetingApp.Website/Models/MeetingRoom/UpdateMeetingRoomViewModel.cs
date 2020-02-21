using MeetingApp.Models.Location;
using MeetingApp.Models.MeetingRoom;
using System.Collections.Generic;

namespace MeetingApp.Website.Models.MeetingRoom
{
    public class UpdateMeetingRoomViewModel
    {
        public UpdateMeetingRoomModel MeetingRoom { get; set; }
        public List<LocationModel> LocationModels { get; set; }
    }
}

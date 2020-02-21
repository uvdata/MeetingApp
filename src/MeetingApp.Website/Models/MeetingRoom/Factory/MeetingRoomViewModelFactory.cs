using MeetingApp.Models.MeetingRoom;
using MeetingApp.Models.Location;
using System.Collections.Generic;
namespace MeetingApp.Website.Models.MeetingRoom.Factory
{
    public class MeetingRoomViewModelFactory
    {
        public static AddMeetingRoomViewModel AddMeetingRoomViewModel(List<LocationModel> locations)
        {
            return new AddMeetingRoomViewModel
            {
                MeetingRoom = new AddMeetingRoomModel(),
                LocationModels = locations
            };
        }

        public static UpdateMeetingRoomViewModel UpdateMeetingRoomViewModel(MeetingRoomModel meetingRoom,
            List<LocationModel> locationModels)
        {
            return new UpdateMeetingRoomViewModel
            {
                MeetingRoom = new UpdateMeetingRoomModel
                {
                    Id = meetingRoom.Id,
                    Name = meetingRoom.Name,
                    Seats = meetingRoom.Seats,
                    Location = new LocationModel
                    {
                        Id = meetingRoom.Location.Id,
                        Name = meetingRoom.Location.Name
                    },
                },
                LocationModels = locationModels
            };
        }
    }
}

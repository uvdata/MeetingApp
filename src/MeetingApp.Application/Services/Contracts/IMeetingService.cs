using MeetingApp.Models.MeetingRoom;
using System.Collections.Generic;
using MeetingApp.Models.Location;

namespace MeetingApp.Application.Services.Contracts
{
    public interface IMeetingService
    {
        List<MeetingRoomModel> ListRooms();
        MeetingRoomModel GetRoom(int id);
        void AddRoom(AddMeetingRoomModel addMeetingRoomModel, int locationId);
        void UpdateRoom(UpdateMeetingRoomModel updateMeetingRoomModel, int meetingRoomId);
        void DeleteRoom(int id);
        LocationModel GetLocation(int id);
        void AddLocation(AddLocationModel addLocationModel);
        void UpdateLocation(UpdateLocationModel updateLocationModel);
        List<LocationModel> ListLocations();
    }
}

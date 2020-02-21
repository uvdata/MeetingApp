using System;
using MeetingApp.Application.Services.Contracts;
using MeetingApp.Database.Repositories.Contracts;
using MeetingApp.Domain;
using MeetingApp.Models.MeetingRoom;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using MeetingApp.Models.Location;

namespace MeetingApp.Application.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRoomRepository _meetingRoomRepository;
        private readonly ILocationRepository _locationRepository;

        private readonly ILogger<MeetingService> _logger;

        public MeetingService(IMeetingRoomRepository meetingRoomRepository, ILocationRepository locationRepository, ILogger<MeetingService> logger)
        {
            _meetingRoomRepository = meetingRoomRepository;
            _logger = logger;
            _locationRepository = locationRepository;
        }

        public void AddRoom(AddMeetingRoomModel addMeetingRoomModel, int locationId)
        {
            var location = _locationRepository.Get(locationId);
            var model = new MeetingRoom
            {
                Name = addMeetingRoomModel.Name,
                Seats = addMeetingRoomModel.Seats,
                LocationId = location.Id,
                CreateDate = DateTime.Now.ToUniversalTime()
            };

            try
            {
                _meetingRoomRepository.Add(model);
                _meetingRoomRepository.Save();
                _logger.LogInformation($"Meeting room with id {model.Id} has been created");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when creating meeting room");
                throw;
            }
        }

        public MeetingRoomModel GetRoom(int id)
        {
            var meetingRoom = _meetingRoomRepository.Get(id);
            return new MeetingRoomModel
            {
                Id = meetingRoom.Id,
                Name = meetingRoom.Name,
                Seats = meetingRoom.Seats,
                Location = new LocationModel
                {
                    Id = meetingRoom.LocationId,
                    Name = meetingRoom.Location.Name
                }
            };
        }

        public List<MeetingRoomModel> ListRooms()
        {
            return _meetingRoomRepository.List().Select(p => new MeetingRoomModel
            {
                Id = p.Id,
                Name = p.Name,
                Seats = p.Seats,
                Location = new LocationModel
                {
                    Name = p.Location.Name,
                    Id = p.Location.Id
                }
            }).ToList();
        }

        public void UpdateRoom(UpdateMeetingRoomModel updateMeetingRoomModel, int locationId)
        {
            try
            {
                var meetingRoom = _meetingRoomRepository.Get(updateMeetingRoomModel.Id);
                var location = _locationRepository.Get(locationId);

                meetingRoom.Name = updateMeetingRoomModel.Name;
                meetingRoom.LocationId = location.Id;
                meetingRoom.Seats = updateMeetingRoomModel.Seats;

                _meetingRoomRepository.Update(meetingRoom);
                _meetingRoomRepository.Save();
                _logger.LogInformation($"Meeting room with id {meetingRoom.Id} has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when updating meeting room");
                throw;
            }
        }

        public void DeleteRoom(int id)
        {
            try
            {
                _meetingRoomRepository.Delete(id);
                _meetingRoomRepository.Save();
                _logger.LogInformation($"Meeting room with id {id} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when deleting meeting room");
                throw;
            }
        }

        public void AddLocation(AddLocationModel addLocationModel)
        {
            var model = new Location
            {
                Name = addLocationModel.Name,
                CreateDate = DateTime.Now.ToUniversalTime()
            };

            try
            {
                _locationRepository.Add(model);
                _locationRepository.Save();
                _logger.LogInformation($"Location with id {model.Id} has been created");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when creating location");
                throw;
            }
        }

        public LocationModel GetLocation(int id)
        {
            var location = _locationRepository.Get(id);
            return new LocationModel
            {
                Id = location.Id,
                Name = location.Name
            };
        }

        public List<LocationModel> ListLocations()
        {
            return _locationRepository.List().Select(p => new LocationModel
            {
                Name = p.Name,
                Id = p.Id
            }).ToList();
        }

        public void UpdateLocation(UpdateLocationModel updateLocationModel)
        {
            try
            {
                var location = _locationRepository.Get(updateLocationModel.Id);

                location.Name = updateLocationModel.Name;

                _locationRepository.Update(location);
                _locationRepository.Save();
                _logger.LogInformation($"Location with id {location.Id} has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when updating location");
                throw;
            }
        }
    }
}

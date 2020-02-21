using MeetingApp.Application.Services.Contracts;
using MeetingApp.Website.Models.MeetingRoom;
using MeetingApp.Website.Models.MeetingRoom.Factory;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Website.Controllers
{
    public class MeetingRoomController : Controller
    {
        private readonly IMeetingService _meetingService;

        public MeetingRoomController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new ListMeetingRoomViewModel
            {
                MeetingRooms = _meetingService.ListRooms()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult MeetingRoom(int id)
        {
            var meetingRoom = _meetingService.GetRoom(id);

            if (meetingRoom == null)
                return new NotFoundResult();

            var vm = new MeetingRoomViewModel();
            return View(vm);

        }

        [HttpGet]
        public IActionResult Add()
        {
            var locations = _meetingService.ListLocations();
            return View(MeetingRoomViewModelFactory.AddMeetingRoomViewModel(locations));
        }

        [HttpPost]
        public IActionResult Add(AddMeetingRoomViewModel addMeetingRoomViewModel)
        {
            if (!ModelState.IsValid)
                return View(addMeetingRoomViewModel);

            _meetingService.AddRoom(addMeetingRoomViewModel.MeetingRoom, addMeetingRoomViewModel.LocationId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var meetingRoom = _meetingService.GetRoom(id);
            if (meetingRoom == null)
                return new NotFoundResult();

            var locations = _meetingService.ListLocations();
            return View(MeetingRoomViewModelFactory.UpdateMeetingRoomViewModel(meetingRoom, locations));

        }

        [HttpPost]
        public IActionResult Update(UpdateMeetingRoomViewModel updateMeetingRoomViewModel, int locationId)
        {
            if (!ModelState.IsValid)
                return View(updateMeetingRoomViewModel);

            _meetingService.UpdateRoom(updateMeetingRoomViewModel.MeetingRoom, locationId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(id);
        }

        [HttpPost]
        public IActionResult DeleteMeetingRoom(int id)
        {
            _meetingService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

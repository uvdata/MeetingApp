using MeetingApp.Application.Services.Contracts;
using MeetingApp.Website.Models.Location;
using MeetingApp.Website.Models.Location.Factory;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Website.Controllers
{
    public class LocationController : Controller
    {
        private readonly IMeetingService _meetingService;

        public LocationController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new ListLocationViewModel()
            {
                Locations = _meetingService.ListLocations()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Location(int id)
        {
            var location = _meetingService.GetLocation(id);

            if (location == null)
                return new NotFoundResult();

            var vm = new LocationViewModel();
            return View(vm);
        } 

        [HttpGet]
        public IActionResult Add()
        {
            return View(LocationViewModelFactory.AddLocationViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddLocationViewModel addLocationViewModel)
        {
            if (!ModelState.IsValid)
                return View(addLocationViewModel);

            _meetingService.AddLocation(addLocationViewModel.Location);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var location = _meetingService.GetLocation(id);
            if (location == null)
                return new NotFoundResult();

            return View(LocationViewModelFactory.UpdateLocationViewModel(location));
        }

        [HttpPost]
        public IActionResult Update(UpdateLocationViewModel updateLocationViewModel)
        {
            if (!ModelState.IsValid)
                return View(updateLocationViewModel);

            _meetingService.UpdateLocation(updateLocationViewModel.Location);
            return RedirectToAction(nameof(Index));
        }
    }
}

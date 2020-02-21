using MeetingApp.Models.Location;

namespace MeetingApp.Website.Models.Location.Factory
{
    public class LocationViewModelFactory
    {
        public static AddLocationViewModel AddLocationViewModel()
        {
            return new AddLocationViewModel
            {
                Location = new AddLocationModel()
            };
        }

        public static UpdateLocationViewModel UpdateLocationViewModel(LocationModel locationModel)
        {
            return new UpdateLocationViewModel
            {
                Location = new UpdateLocationModel
                {
                    Id = locationModel.Id,
                    Name = locationModel.Name
                }
            };
        }
    }
}

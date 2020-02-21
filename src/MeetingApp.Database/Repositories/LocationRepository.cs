using MeetingApp.Database.Context;
using MeetingApp.Database.Repositories.Contracts;
using MeetingApp.Domain;

namespace MeetingApp.Database.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(EfContext context) : base(context)
        {
        }
    }
}

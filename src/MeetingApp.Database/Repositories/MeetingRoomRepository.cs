using MeetingApp.Database.Context;
using MeetingApp.Database.Repositories.Contracts;
using MeetingApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MeetingApp.Database.Repositories
{
    public class MeetingRoomRepository : BaseRepository<MeetingRoom>, IMeetingRoomRepository
    {
        public MeetingRoomRepository(EfContext context) : base(context)
        {
        }

        public override IEnumerable<MeetingRoom> List()
        {
            return DbSet.Include(p => p.Location).ToList();
        }

        public override MeetingRoom Get(int id)
        {
            return DbSet.Include(p => p.Location).FirstOrDefault(p => p.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MeetingApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Database.Context
{
    public static class DbInitializer
    {
        public static void Initialize(EfContext context)
        {
            context.Database.Migrate();

            var changes = false;
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(new List<Location>
                {
                    new Location
                    {
                        CreateDate = DateTime.Now.ToUniversalTime(),
                        Name = "Aros"
                    },
                    new Location
                    {
                        CreateDate = DateTime.Now.ToUniversalTime(),
                        Name = "Reepark"
                    }
                });
                changes = true;
            }

            if (!context.MeetingRooms.Any())
            {
                context.MeetingRooms.AddRange(
                    new List<MeetingRoom>
            {
                new MeetingRoom
                {
                    CreateDate = DateTime.Now.ToUniversalTime(),
                    Name = "Abernes hangout",
                    LocationId = 1,
                    Seats = 10
                }
            });
                changes = true;
            }

            if (changes)
            {
                context.SaveChanges();
            }
        }
    }
}

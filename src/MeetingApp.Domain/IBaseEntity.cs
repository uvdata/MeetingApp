using System;

namespace MeetingApp.Domain
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}

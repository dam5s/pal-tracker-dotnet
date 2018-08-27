using System;

namespace PalTracker
{
    public struct TimeEntry
    {
        public long? Id { get; set; }
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }


        public TimeEntry(long? id, long projectId, long userId, DateTime date, int hours): this()
        {
            Id = id;
            ProjectId = projectId;
            UserId = userId;
            Date = date;
            Hours = hours;
        }

        public TimeEntry(long projectId, long userId, DateTime date, int hours)
            : this(null, projectId, userId, date, hours)
        {
        }
    }
}

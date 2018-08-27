using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext _context;

        public MySqlTimeEntryRepository(TimeEntryContext context)
        {
            _context = context;
        }


        public TimeEntry Create(TimeEntry timeEntry)
        {
            var fields = timeEntry.ToRecord();
            var entry = _context.TimeEntryRecords.Add(fields);

            _context.SaveChanges();

            return entry.Entity.ToEntity();
        }

        public TimeEntry Find(long id) =>
            FindRecord(id).ToEntity();

        public bool Contains(long id) =>
            _context.TimeEntryRecords
                .AsNoTracking()
                .Any(record => record.Id == id);

        public IEnumerable<TimeEntry> List() =>
            _context.TimeEntryRecords
                .AsNoTracking()
                .ToList()
                .Select(record => record.ToEntity());

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var updates = timeEntry.ToRecordWithId(id);
            var entry = _context.TimeEntryRecords.Update(updates);

            _context.SaveChanges();

            return entry.Entity.ToEntity();
        }

        public void Delete(long id)
        {
            _context.TimeEntryRecords.Remove(FindRecord(id));
            _context.SaveChanges();
        }


        private TimeEntryRecord FindRecord(long id)
        {
            return _context.TimeEntryRecords.Single(record => record.Id == id);
        }
    }
}

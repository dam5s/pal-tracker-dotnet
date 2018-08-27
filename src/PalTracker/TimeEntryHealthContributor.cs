using System.Linq;
using Steeltoe.Common.HealthChecks;
using static Steeltoe.Common.HealthChecks.HealthStatus;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        public const int MaxTimeEntries = 5;

        public TimeEntryHealthContributor(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }


        public HealthCheckResult Health()
        {
            var count = _timeEntryRepository.List().Count();
            var status = count < MaxTimeEntries ? UP : DOWN;
            var result = new HealthCheckResult {Status = status};
            
            result.Details.Add("threshold", MaxTimeEntries);
            result.Details.Add("count", count);
            result.Details.Add("status", status.ToString());

            return result;
        }

        public string Id { get; } = "timeEntry";
    }
}

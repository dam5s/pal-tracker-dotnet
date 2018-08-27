using System.Collections.Generic;

namespace PalTracker
{
    public class OperationCounter<T> : IOperationCounter<T>
    {
        public void Increment(TrackedOperation operation)
        {
            GetCounts[operation] = GetCounts[operation] + 1;
        }

        public IDictionary<TrackedOperation, int> GetCounts { get; } = new Dictionary<TrackedOperation, int>
        {
            [TrackedOperation.Create] = 0,
            [TrackedOperation.Read] = 0,
            [TrackedOperation.List] = 0,
            [TrackedOperation.Update] = 0,
            [TrackedOperation.Delete] = 0
        };

        public string Name { get; } = typeof(T).Name + "Operations";
    }
}

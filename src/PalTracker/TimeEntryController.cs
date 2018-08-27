using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _repository;
        private readonly IOperationCounter<TimeEntry> _counter;

        public TimeEntryController(ITimeEntryRepository repository, IOperationCounter<TimeEntry> counter)
        {
            _repository = repository;
            _counter = counter;
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            _counter.Increment(TrackedOperation.Read);
            
            if (_repository.Contains(id))
            {
                return Ok(_repository.Find(id));
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry fields)
        {
            _counter.Increment(TrackedOperation.Create);

            var created = _repository.Create(fields);
            return CreatedAtRoute("GetTimeEntry", new {id = created.Id}, created);
        }

        [HttpGet]
        public IActionResult List()
        {
            _counter.Increment(TrackedOperation.List);

            return Ok(_repository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry fields)
        {
            _counter.Increment(TrackedOperation.Update);

            if (_repository.Contains(id))
            {
                return Ok(_repository.Update(id, fields));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _counter.Increment(TrackedOperation.Delete);

            if (_repository.Contains(id))
            {
                _repository.Delete(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}

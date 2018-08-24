using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _repository;

        public TimeEntryController(ITimeEntryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            if (_repository.Contains(id))
            {
                return Ok(_repository.Find(id));
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry fields)
        {
            var created = _repository.Create(fields);
            return CreatedAtRoute("GetTimeEntry", new {id = created.Id}, created);
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_repository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry fields)
        {
            if (_repository.Contains(id))
            {
                return Ok(_repository.Update(id, fields));
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (_repository.Contains(id))
            {
                _repository.Delete(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}

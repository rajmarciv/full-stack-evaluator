using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Data;

namespace TaskManager.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }
        //kukunin nya ung task
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }
        // mag create sya ng task 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }
        //update nya ung task per ID
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem updated)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = updated.Title;
            task.IsDone = updated.IsDone;
            await _context.SaveChangesAsync();

            return Ok(task);
        }
        //Delete nya ung task per ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

//Note: pero ung way ko naman po, bago ko sya i-implement sa front-end, e-test ko muna sa json response then saka ako mag implement sa front-end
// eigther bootstrap 5 admin template or react templatew.

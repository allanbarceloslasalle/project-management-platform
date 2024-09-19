using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Injecting ApplicationDbContext via constructor
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            // Fetching all tasks from the database
            var tasks = await _context.ProjectTasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .ToListAsync();

            return Ok(tasks);
        }

        // GET: /api/task/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTask(int id)
        {
            // Fetching a specific task by id
            var task = await _context.ProjectTasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound(); // If no task is found, return 404
            }

            return Ok(task); // Returning the task
        }

        
        // /api/task
        [HttpPost]
        public String CreateTask()
        {
            return "";
        }

        
        // /api/task
        [HttpPut("{id}")]
        public String UpdateTask(int id, String task)
        {
            return "";
        }
        
        // /api/task
        [HttpDelete("{id}")]
        public String DeleteTask(int id)
        {
            return "";
        }
    
    }
}
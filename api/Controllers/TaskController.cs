using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
// CRUD -> Create Read, Update, Delete
// 99% os projects you will see in your life  follow this pattern

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase {

        private readonly ApplicationDbContext _context;
        
        public TaskController(ApplicationDbContext context){ // Inject the dependency
            _context = context;
        }


        [HttpGet] // TO GET somenting
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks() // Pagination 
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        [HttpGet("{id}")] // TO GET
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<ProjectTask>> GetTask(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);

            if(task == null)
                return NotFound();

            return task;
        }

        
        // /api/task
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<ProjectTask>> CreateTask(ProjectTask task)
        {
            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new {id = task.Id}, task);
        }

        
        // PUT -> Update ALL
        // PATCH -> Update Partials
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<IActionResult> UpdateTask(int id, ProjectTask task)
        {
            if(id != task.Id)
                return BadRequest();
            
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        
        [HttpDelete("{id}")] // soft-delete
        [Authorize(Roles = "Admin,Manager")] 
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);

            if(task == null)
                return NotFound();

            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
    }
}
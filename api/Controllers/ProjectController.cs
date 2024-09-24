using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// CRUD -> Create Read, Update, Delete
// 99% os projects you will see in your life  follow this pattern

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase {

        private readonly ApplicationDbContext _context;
        
        public ProjectController(ApplicationDbContext context){ // Inject the dependency
            _context = context;
        }


        [HttpGet] // TO GET somenting
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects() // Pagination 
        {
            return await _context.Projects.ToListAsync();
        }

        [HttpGet("{id}")] // TO GET
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var task = await _context.Projects.FindAsync(id);

            if(task == null)
                return NotFound();

            return task;
        }


        // /api/blog-post/123/comments
        [HttpGet("{id}/tasks")] // TO GET
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjecttasks(int id)
        {

            var task = await _context.Projects.FindAsync(id);

            if (task == null)
                return NotFound();

            return task.ProjectTasks.ToList();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new {id = project.Id}, project);
        }

        
        // PUT -> Update ALL
        // PATCH -> Update Partials
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if(id != project.Id)
                return BadRequest();
            
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if(project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
    }
}
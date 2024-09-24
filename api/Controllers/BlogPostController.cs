using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// CRUD -> Create Read, Update, Delete
// 99% os blogPosts you will see in your life follow this pattern
 
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
 
        private readonly ApplicationDbContext _context;
 
        public BlogPostController(ApplicationDbContext context)
        { // Inject the dependency
            _context = context;
        }
 
 
        [HttpGet] // TO GET something
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts() // Pagination
        {
            return await _context.BlogPosts.ToListAsync();
        }
 
        [HttpGet("{id}")] // TO GET specific blog post
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
 
            if (blogPost == null)
                return NotFound();
 
            return blogPost;
        }
 
        // /api/blog-post/123/comments
        [HttpGet("{id}/comments")] // TO GET comments of a blog post
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<IEnumerable<BlogPostComment>>> GetBlogPostComments(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
 
            if (blogPost == null)
                return NotFound();
 
            return blogPost.comments.ToList();
        }
 
        [HttpPost] // TO CREATE a new blog post
        [Authorize(Roles = "Admin,Manager, User")]
        public async Task<ActionResult<BlogPost>> CreateBlogPost(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
        }
 
        // PUT -> Update ALL
        // PATCH -> Update Partials
        [HttpPut("{id}")] // TO UPDATE an existing blog post
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
                return BadRequest();
 
            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();
 
            return NoContent();
        }
 
        [HttpDelete("{id}")] // soft-delete a blog post
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
 
            if (blogPost == null)
                return NotFound();
 
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
 
            return NoContent();
        }
 
    }
}
 

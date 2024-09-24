using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// CRUD -> Create Read, Update, Delete
// 99% os blogPosts you will see in your life  follow this pattern

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


        [HttpGet] // TO GET somenting
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts() // Pagination 
        {
            return await _context.BlogPosts.ToListAsync();
        }

        [HttpGet("{id}")] // TO GET
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
                return NotFound();

            return blogPost;
        }


        // /api/blog-post/123/comments
        [HttpGet("{id}/comments")] // TO GET
        public async Task<ActionResult<IEnumerable<BlogPostComment>>> GetBlogPostComments(int id)
        {

            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
                return NotFound();

            return blogPost.comments.ToList();
        }


        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreateBlogPost(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
        }


        // PUT -> Update ALL
        // PATCH -> Update Partials
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
                return BadRequest();

            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")] // soft-delete 
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
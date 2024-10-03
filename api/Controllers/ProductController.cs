using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// CRUD -> Create Read, Update, Delete
// 99% os products you will see in your life  follow this pattern

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase {

        private readonly ApplicationDbContext _context;
        
        public ProductController(ApplicationDbContext context){ // Inject the dependency
            _context = context;
        }


        [HttpGet] // TO GET somenting
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() // Pagination 
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")] // TO GET
        [Authorize(Roles = "Admin,Manager,User,Developer")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var task = await _context.Products.FindAsync(id);

            if(task == null)
                return NotFound();

            return task;
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }

        
        // PUT -> Update ALL
        // PATCH -> Update Partials
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if(id != product.Id)
                return BadRequest();
            
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        
        [HttpDelete("{id}")] // soft-delete 
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    
    }
}
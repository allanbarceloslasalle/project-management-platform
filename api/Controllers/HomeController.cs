using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "API working"});
        }

        [HttpGet("protected")]
        [Authorize(Roles = "Admin")]
        public IActionResult Protected()
        {
            return Ok(new { message = "This is a protected route"});
        }
    }
}
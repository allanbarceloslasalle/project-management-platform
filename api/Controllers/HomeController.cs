using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

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
    }
}
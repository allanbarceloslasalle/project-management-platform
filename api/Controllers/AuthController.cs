using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model){
            if (model.Password != model.ConfirmPassword){
                return BadRequest("Passwortds do not match.");
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                return Ok(result.ToString());
            }

            return BadRequest(result.Errors);
        }

        // /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model){
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            if(result.Succeeded){
                var user = await _userManager.FindByEmailAsync(model.Email);
                var token = await GenerateJwtToken(user);
                return Ok(new {Token = token});
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(IdentityUser user){

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"])); // salt
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ApplicationDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    return Unauthorized();
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, "An internal server error occured.");
            }
 
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _logger.LogInformation("Starting sign-up process for user: {UserName}", signupDto.UserName);

                var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.UserName == signupDto.UserName);
                if (existingUser != null)
                {
                    return BadRequest(new { Message = "Username is already taken" });
                }

                var user = new User
                {
                    UserName = signupDto.UserName,
                    Email = signupDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(signupDto.Password),
                    IsAdmin = signupDto.IsAdmin,
                    IsBlocked = false //default value for new users
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "User registered successfully" });
            }

            catch(DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "An Error occurred during signup");
                return StatusCode(500, "A databse error occurred.");
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "An Error occurred during signup");
                return StatusCode(500, "An internal server error occurred.");
            }
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.UserName),
                new Claim ("IsAdmin", user.IsAdmin.ToString()),
                new Claim (ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

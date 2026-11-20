using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RaceDayDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(RaceDayDbContext context, IAuthService authService)
        {
            _context = context; // Assign Entity Framework database context
            _authService = authService; // Assign authentication and password hashing service (Microsoft, 2024)
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("User with this email already exists."); // Return HTTP 400 if duplicate email is found (Microsoft, 2024)
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                RoleId = request.RoleId,
                PasswordHash = _authService.HashPassword(request.Password) // Securely hash password before persistence (Microsoft, 2024)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully", userId = user.UserId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            // Validate user existence and verify password hash integrity (Microsoft, 2024)
            if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password.");
            }

            // Return HTTP 200 OK along with user session details and role metadata (Microsoft, 2024)
            return Ok(new
            {
                message = "Login successful",
                userId = user.UserId,
                fullName = user.FullName,
                role = user.Role?.RoleName
            });
        }
    }
}
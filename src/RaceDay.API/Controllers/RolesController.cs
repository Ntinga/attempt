using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    // API Controller handling user role management operations (Microsoft, 2024)
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        // Initializes a new instance of RolesController with database context (Microsoft, 2024)
        public RolesController(RaceDayDbContext context)
        {
            _context = context;
        }

        // Retrieves all configured system roles (Microsoft, 2024)
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        // Creates a new user role within the system (Microsoft, 2024)
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto request)
        {
            if (await _context.Roles.AnyAsync(r => r.RoleName == request.RoleName))
            {
                return BadRequest("Role already exists.");
            }

            var role = new Role
            {
                RoleName = request.RoleName
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Role created successfully", roleId = role.RoleId });
        }
    }
}
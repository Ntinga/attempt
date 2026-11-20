using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    // API Controller handling event enrolment operations (Microsoft, 2024)
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EnrolmentsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // Retrieves all event enrolments with related user and event details (Microsoft, 2024)
        [HttpGet]
        public async Task<IActionResult> GetEnrolments()
        {
            var enrolments = await _context.Enrolments
                .Include(e => e.Participant)
                .Include(e => e.Category)
                .ThenInclude(c => c.Event)
                .ToListAsync();

            return Ok(enrolments);
        }

        // Enrols a user into a specific racing event (Microsoft, 2024)
        [HttpPost]
        public async Task<IActionResult> CreateEnrolment([FromBody] EnrolmentCreateDto request)
        {
            // Verify if user exists (Microsoft, 2024)
            var participantExists = await _context.Users.AnyAsync(u => u.UserId == request.ParticipantId);
            if (!participantExists)
            {
                return BadRequest("Specified user does not exist.");
            }

            // Verify if category exists
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == request.CategoryId);
            if (!categoryExists)
            {
                return BadRequest("Specified category does not exist.");
            }

            // Check for duplicate enrolment (Microsoft, 2024)
            var alreadyEnrolled = await _context.Enrolments
                .AnyAsync(e => e.ParticipantId == request.ParticipantId && e.CategoryId == request.CategoryId);
            if (alreadyEnrolled)
            {
                return BadRequest("User is already enrolled in this event.");
            }

            var enrolment = new Enrolment
            {
                ParticipantId = request.ParticipantId,
                CategoryId = request.CategoryId,
                EnrolmentDate = DateTime.UtcNow
            };

            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Enrolled successfully", enrolmentId = enrolment.EnrolmentId });
        }
    }
}
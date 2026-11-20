using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    // API Controller handling event management operations (Microsoft, 2024)
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // Retrieves a list of all scheduled racing events (Microsoft, 2024)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEvents()
        {
                var events = await _context.Events
                .Include(e => e.Categories)
                .Include(e => e.Organiser)
                .ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var raceEvent = await _context.Events
                .Include(e => e.Categories)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            return Ok(raceEvent);
        }

        // Creates a new racing event in the system (Microsoft, 2024)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto request)
        {
            var newEvent = new Event
            {
                EventName = request.EventName,
                Description = request.Description,
                EventDate = request.EventDate,
                Location = request.Location,
                OrganiserId = request.OrganiserId,
                BannerUrl = request.BannerUrl
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Event created successfully", eventId = newEvent.EventId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventCreateDto request)
        {
            var raceEvent =await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            raceEvent.EventName = request.EventName;

            raceEvent.Description = request.Description;

            raceEvent.EventDate = request.EventDate;

            raceEvent.Location =request.Location;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(raceEvent);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var raceEvent = await _context.Events
                .Include(e => e.Categories)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            return Ok(raceEvent);
        }
    }
}
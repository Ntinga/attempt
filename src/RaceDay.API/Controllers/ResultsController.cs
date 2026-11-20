using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    // API Controller handling event result recording and performance retrieval (Microsoft, 2024)
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // Retrieves all recorded event results with related enrolment, user, and event details (Microsoft, 2024)
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            var results = await _context.Results
                .Include(r => r.Enrolment)
                .ThenInclude(e => e.Participant)
                .Include(r => r.Enrolment)
                .ThenInclude(e => e.Category)
                .ThenInclude(c => c.Event)
                .ToListAsync();

            return Ok(results);
        }

        // Records a race result for a participant's event enrolment (Microsoft, 2024)
        [HttpPost]
        public async Task<IActionResult> CreateResult([FromBody] ResultCreateDto request)
        {
            // Verify if the enrolment exists (Microsoft, 2024)
            var enrolmentExists = await _context.Enrolments.AnyAsync(e => e.EnrolmentId == request.EnrolmentId);
            if (!enrolmentExists)
            {
                return BadRequest("Specified event enrolment does not exist.");
            }

            // Check if a result has already been recorded for this enrolment (Microsoft, 2024)
            var resultExists = await _context.Results.AnyAsync(r => r.EnrolmentId == request.EnrolmentId);
            if (resultExists)
            {
                return BadRequest("A result has already been recorded for this enrolment.");
            }

            var result = new Result
            {
                EnrolmentId = request.EnrolmentId,
                FinishTime = request.FinishTime,
                Position = request.Position
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Result recorded successfully", resultId = result.ResultId });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly
            IBlobStorageService
            _blobStorageService;

        public FilesController(
            IBlobStorageService blobStorageService)
        {
            _blobStorageService =
                blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult>
            Upload(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest(
                    "No file selected.");
            }

            var fileUrl =
                await _blobStorageService
                    .UploadFileAsync(
                        file,
                        "raceday");

            return Ok(
                new
                {
                    fileUrl
                });
        }
    }
}
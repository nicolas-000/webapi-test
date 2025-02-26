using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using WebApiTest.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController(IFileService fileService) : ControllerBase
    {
        private readonly IFileService _fileService = fileService;

        [HttpGet("read/{filename}")]
        public async Task<IActionResult> ReadFile(string filename)
        {
            var content = await _fileService.GetFileContentAsync(filename);
            return Ok(new { Content = content });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFile([FromBody] FileRequest request)
        {
            if (string.IsNullOrEmpty(request.Filename))
            {
                return BadRequest("Filename is required.");
            }

            var filePath = await _fileService.CreateFileAsync(request.Filename, request.Content);
            return Ok(new { FilePath = filePath });
        }

        [HttpPut("edit/{filename}")]
        public async Task<IActionResult> EditFile([FromBody] FileRequest request)
        {
            var filePath = await _fileService.EditFileAsync(request.Filename, request.Content);
            return Ok(new { FilePath = filePath });
        }

        [HttpPut("rename/{filename}")]
        public async Task<IActionResult> RenameFile(string filename, string newFilename)
        {
            var result = await _fileService.RenameFileAsync(filename, newFilename);
            if (!result)
            {
                return NotFound("File not found or new filename already exists.");
            }
            return Ok("File renamed successfully.");
        }
    }
}

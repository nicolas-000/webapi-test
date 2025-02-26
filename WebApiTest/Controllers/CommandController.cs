using System.Security;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandController(CommandService commandService) : ControllerBase
    {
        private readonly CommandService _commandService = commandService;

        [HttpPost("execute")]
        public IActionResult Execute([FromBody] string command)
        {
            try
            {
                string result = _commandService.ExecuteCommand(command);
                return Ok(result);
            }
            catch (SecurityException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

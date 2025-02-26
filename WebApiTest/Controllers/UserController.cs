using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("Private")]
        [Authorize]
        public IActionResult PrivateEndpoint()
        {
            var currentUser = _userService.GetCurrentUser(HttpContext.User);

            return Ok($"Hi {currentUser.Name}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Public");
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            var currentUser = _userService.GetCurrentUser(HttpContext.User);

            return Ok($"Hi {currentUser.Name}");
        }

        [HttpGet("User")]
        [Authorize(Roles = "User")]
        public IActionResult UserEndpoint()
        {
            var currentUser = _userService.GetCurrentUser(HttpContext.User);

            return Ok($"Hi {currentUser.Name}");
        }
    }
}

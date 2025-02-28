using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
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

        [HttpGet("all")]
        [Authorize(Roles = "DueñoWeb,Administrador")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("role/{roleName}")]
        [Authorize(Roles = "DueñoWeb,Administrador")]
        public ActionResult<IEnumerable<User>> GetUsersByRole(string roleName)
        {
            var users = _userService.GetUsersByRole(roleName);
            return Ok(users);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "DueñoWeb,Administrador,Empleado")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var currentUser = _userService.GetCurrentUser(HttpContext.User);
            if (currentUser.Id != id && !User.IsInRole("DueñoWeb") && !User.IsInRole("Administrador"))
            {
                return Forbid();
            }

            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "DueñoWeb")]
        public ActionResult<User> CreateUser(User createUserDto)
        {
            // TODO

            return Created();
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "DueñoWeb,Administrador")]
        public IActionResult UpdateUser(Guid id, User updateUserDto)
        {
            if (updateUserDto.Role == "DueñoWeb" && !User.IsInRole("DueñoWeb"))
            {
                return Forbid();
            }

            // TODO

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "DueñoWeb")]
        public IActionResult DeleteUser(Guid id)
        {
            // TODO

            return NoContent();
        }
    }
}

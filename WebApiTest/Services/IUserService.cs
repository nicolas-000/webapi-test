using System.Security.Claims;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public interface IUserService
    {
        User GetCurrentUser(ClaimsPrincipal user);
    }
}

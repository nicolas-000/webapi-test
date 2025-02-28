using System.Security.Claims;
using DocumentFormat.OpenXml.Office2010.Excel;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public interface IUserService
    {
        User GetCurrentUser(ClaimsPrincipal user);
        List<User> GetAllUsers();
        List<User> GetUsersByRole(string roleName);
        User GetUserById(Guid id);
    }
}

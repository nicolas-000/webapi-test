using System.Security.Claims;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public class UserService : IUserService
    {
        public User GetCurrentUser(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Id = Guid.Parse(userClaims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value),
                    Username = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value,
                    Email = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.GivenName)?.Value,
                    Phone = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.MobilePhone)?.Value,
                    Role = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.Role)?.Value
                };
            }

            return null;
        }

        public List<User> GetAllUsers()
        {
            return UserConstants.Users.ToList();
        }

        public List<User> GetUsersByRole(string roleName)
        {
            return UserConstants.Users.Where(u => u.Role.ToLower() == roleName.ToLower()).ToList();
        }

        public User GetUserById(Guid id)
        {
            return UserConstants.Users.Find(u => u.Id == id);
        }
    }
}

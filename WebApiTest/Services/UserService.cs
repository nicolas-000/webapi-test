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
                    Username = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.GivenName)?.Value,
                    Phone = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.MobilePhone)?.Value,
                    Role = userClaims.FirstOrDefault(i => i.Type == ClaimTypes.Role)?.Value
                };
            }

            return null;
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public class LoginService(IConfiguration config) : ILoginService
    {
        private readonly IConfiguration _config = config;

        public async Task<string> Login(UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);

            if (user != null)
            {
                var token = await Generate(user);
                return token;
            }

            return null;
        }

        private async Task<string> Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(UserLogin userLogin)
        {
            var user = UserConstants.Users.FirstOrDefault(u => u.Username.ToLower() == userLogin.Username.ToLower() && u.Password == userLogin.Password);

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}

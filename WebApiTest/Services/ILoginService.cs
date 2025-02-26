using WebApiTest.Models;

namespace WebApiTest.Services
{
    public interface ILoginService
    {
        Task<string> Login(UserLogin userLogin);
    }
}

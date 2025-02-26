namespace WebApiTest.Models
{
    public class UserConstants
    {
        public static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "admin123",
                Name = "Nombre Admin",
                Email = "admin@mail.com",
                Phone = "987654321",
                Role = "Admin"
            },
            new User()
            {
                Id = Guid.NewGuid(),
                Username = "user",
                Password = "user123",
                Name = "Nombre Usuario",
                Email = "correo@mail.com",
                Phone = "123456789",
                Role = "User"
            }
        };
    }
}

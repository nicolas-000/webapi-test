namespace WebApiTest.Models
{
    public static class UserConstants
    {
        public static List<User> Users = new List<User>()
        {
            new User()
            {
                Id = Guid.Parse("f65c8326-83c8-4785-a9dd-fc5835417d33"),
                Username = "Dueño",
                Password = "Dueño123",
                Name = "Nombre Dueño",
                Email = "dueño@mail.com",
                Phone = "987654321",
                Role = "DueñoWeb"
            },
            new User()
            {
                Id = Guid.Parse("409f4e96-0329-4e78-8e70-40f839c062fd"),
                Username = "Admin",
                Password = "Admin123",
                Name = "Nombre Admin",
                Email = "admin@mail.com",
                Phone = "123456789",
                Role = "Administrador"
            },
            new User()
            {
                Id = Guid.Parse("c8cc7802-1b8f-43ac-913a-e90efe08ab98"),
                Username = "Empleado",
                Password = "Empleado123",
                Name = "Nombre Empleado",
                Email = "empleado@mail.com",
                Phone = "789123456",
                Role = "Empleado"
            }
        };
    }
}

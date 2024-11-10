using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Entities.Users;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Users.Dtos;

public class CreateUser
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Roles Role { get; set; }

    public List<User> Users { get; set; }

    public static implicit operator User(CreateUser create)
    {
        return new User
        {
            Id = create.Id,
            Name = create.Name,
            Password = create?.Password,
            Email = create.Email,
            Role = create.Role,
                
        };
    }
}


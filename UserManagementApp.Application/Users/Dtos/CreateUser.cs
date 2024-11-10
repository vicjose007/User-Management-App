using System.Text.Json.Serialization;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Entities.Users;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Users.Dtos;

public class CreateUser
{
    [JsonIgnore]
    public string? Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Roles Role { get; set; }

    [JsonIgnore]
    public bool? IsActive { get; set; }

    public List<CreatePhone> Phones { get; set; }

    public static implicit operator User(CreateUser create)
    {
        return new User
        {
            Id = create?.Id,
            Name = create.Name,
            Password = create?.Password,
            Email = create.Email,
            Role = create.Role,
            LastLogin = DateTime.Now,
                
        };
    }
}


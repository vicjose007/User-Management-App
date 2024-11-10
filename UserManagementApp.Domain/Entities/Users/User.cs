using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UserManagementApp.Domain.Entities.Phones;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Domain.Entities.Users;

public class User : Entity
{
    public string Name { get; set; } 

    public string Email { get; set; } 

    public string Password { get; set; }

    public Roles Role { get; set; }

    public List<Phone> Phones { get; set; }
}

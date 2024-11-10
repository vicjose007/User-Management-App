using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Application.Users.Dtos;

public class GetUser
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Roles Role { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? Updated { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool IsActive { get; set; }

    public List<GetPhone> Phones { get; set; }
}


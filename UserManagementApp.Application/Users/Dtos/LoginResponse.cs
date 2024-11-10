namespace UserManagementApp.Application.Users.Dtos;

public class LoginResponse
{
    public string Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? Modified { get; set; }

    public DateTime? Last_login { get; set; }

    public string Token { get; set; }

    public bool IsActive { get; set; }
}

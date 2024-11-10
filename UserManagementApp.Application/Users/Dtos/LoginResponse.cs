namespace UserManagementApp.Application.Users.Dtos;

public class LoginResponse
{
    public string Token { get; set; }

    public GetUser User { get; set; }
}

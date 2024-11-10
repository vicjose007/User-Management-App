using UserManagementApp.Application.Users.Dtos;

namespace UserManagementApp.Application.Phones.Dtos;

public class GetPhone
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public GetUser User { get; set; }

    public string Number { get; set; }

    public string CityCode { get; set; }

    public string ContryCode { get; set; }
}


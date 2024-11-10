using UserManagementApp.Domain.Entities.Users;

namespace UserManagementApp.Domain.Entities.Phones;

public class Phone : Entity
{
    public string UserId { get; set; }

    public User User { get; set; }

    public string Number { get; set; }

    public string CityCode { get; set; }

    public string ContryCode { get; set; }

}
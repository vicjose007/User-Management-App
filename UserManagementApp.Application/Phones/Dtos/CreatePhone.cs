using UserManagementApp.Domain.Entities.Phones;

namespace UserManagementApp.Application.Phones.Dtos;

public class CreatePhone
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Number { get; set; }

    public string CityCode { get; set; }

    public string ContryCode { get; set; }

    public static implicit operator Phone(CreatePhone create)
    {
        return new Phone
        {
            Id = create.Id,
            UserId = create.UserId,
            Number = create.Number,
            CityCode = create.CityCode,
            ContryCode = create.ContryCode,
                
        };
    }
}


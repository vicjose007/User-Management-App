using System.Text.Json.Serialization;
using UserManagementApp.Domain.Entities.Phones;

namespace UserManagementApp.Application.Phones.Dtos;

public class CreateUserPhone
{
    [JsonIgnore]
    public string? Id { get; set; } = null!;

    public string? UserId { get; set; } = null!;

    public string Number { get; set; }

    public string CityCode { get; set; }

    public string ContryCode { get; set; }

    public static implicit operator Phone(CreateUserPhone create)
    {
        return new Phone
        {
            Id = create?.Id,
            UserId = create?.UserId,
            Number = create.Number,
            CityCode = create.CityCode,
            ContryCode = create.ContryCode,

        };
    }
}
using System.Linq.Expressions;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Domain.Entities.Phones;

namespace UserManagementApp.Application.Phones.Services.Projections;

public static class PhoneProjection
{
    public static Expression<Func<Phone, GetPhone>> GetAll { get; } = element =>
        new GetPhone
        {
            Id = element.Id,
            UserId= element.UserId,
            Number = element.Number,
            User = new Users.Dtos.GetUser
            {
                Id= element.User.Id,
                Name= element.User.Name,
                Email= element.User.Email,
                Role= element.User.Role,            
            },
            CityCode= element.CityCode,
            ContryCode= element.ContryCode,
        };

}


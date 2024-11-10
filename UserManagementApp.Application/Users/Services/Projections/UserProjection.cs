using System.Linq.Expressions;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Entities.Users;

namespace UserManagementApp.Application.Users.Services.Projections;

public static class UserProjection
{
    public static Expression<Func<User, GetUser>> GetAll { get; } = element =>
        new GetUser
        {
            Id = element.Id,
            Name= element.Name,
            Email= element.Email,
            Role= element.Role,
            Phones = element.Phones.Select(p => new GetPhone
            {
                Id = p.Id,
                UserId = p.UserId,
                CityCode = p.CityCode,
                ContryCode= p.ContryCode,
                Number= p.Number,
            }).ToList()

        };

}


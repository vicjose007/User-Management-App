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
            Name = element.Name,
            Email = element.Email,
            Role = element.Role,
            Created = element.Created,
            Updated = element.Updated,
            IsActive = element.IsActive,
            LastLogin = element.LastLogin,
            Password = element.Password,
            Phones = element.Phones.Select(p => new GetPhone
            {
                Id = p.Id,
                UserId = p.UserId,
                CityCode = p.CityCode,
                ContryCode= p.ContryCode,
                Number= p.Number,
            }).ToList()

        };

    public static Expression<Func<User, GetUser>> GetMinorDetails { get; } = element =>
    new GetUser
    {
        Id = element.Id,
        Name = element.Name,
        Email = element.Email,
        Password = element.Password,
        Phones = element.Phones.Select(p => new GetPhone
        {
            Id = p.Id,
            UserId = p.UserId,
            CityCode = p.CityCode,
            ContryCode = p.ContryCode,
            Number = p.Number,
        }).ToList()

    };

}


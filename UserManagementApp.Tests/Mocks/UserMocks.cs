using AutoFixture;
using UserManagementApp.Domain.Entities.Users;
using UserManagementApp.Infrastructure.Context;
using UserManagementApp.Infrastructure.Repositories.Users;

namespace UserManagementApp.Tests.Mocks;

public class UserMocks
{
    public static UserRepository GetUserRepository(UserManagementAppDbContext contextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var users = fixture.CreateMany<User>().ToList();

        users.Add(fixture.Build<User>()
            .With(ur => ur.Id, "b04b00c3-03be-48fb-9453-dee35c3c9c87")
            .With(ur => ur.Email, "testuser@example.com") // Aquí se agrega el email
            .Create());

        contextFake.Users.AddRange(users);
        contextFake.SaveChanges();

        return new UserRepository(contextFake);
    }
}

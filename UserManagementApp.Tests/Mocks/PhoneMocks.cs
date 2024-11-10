using AutoFixture;
using UserManagementApp.Domain.Entities.Phones;
using UserManagementApp.Infrastructure.Context;
using UserManagementApp.Infrastructure.Repositories.Phones;

namespace UserManagementApp.Tests.Mocks;

public class PhoneMocks
{
    public static PhoneRepository GetPhoneRepository(UserManagementAppDbContext contextFake)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var phones = fixture.CreateMany<Phone>().ToList();

        phones.Add(fixture.Build<Phone>()
            .With(ur => ur.Id, "06076739-f0ea-4554-9a35-003d100eaa52").Create());

        contextFake.Phones.AddRange(phones);
        contextFake.SaveChanges();

        return new PhoneRepository(contextFake);
    }
}

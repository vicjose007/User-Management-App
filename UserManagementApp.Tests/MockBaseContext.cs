using Microsoft.EntityFrameworkCore;
using UserManagementApp.Infrastructure.Context;

namespace UserManagementApp.Tests;
public static class MockBaseContext
{
    public static UserManagementAppDbContext Get()
    {
        var options = new DbContextOptionsBuilder<UserManagementAppDbContext>()
            .UseInMemoryDatabase(databaseName: $"VirtEduDbContext-{Guid.NewGuid()}")
            .Options;

        var contextFake = new UserManagementAppDbContext(options);
        contextFake.Database.EnsureDeleted();

        return contextFake;
    }
}

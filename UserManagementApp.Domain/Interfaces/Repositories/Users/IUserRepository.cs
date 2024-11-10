using UserManagementApp.Domain.Entities.Users;

namespace UserManagementApp.Domain.Interfaces.Repositories.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}


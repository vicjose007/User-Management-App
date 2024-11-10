using Microsoft.EntityFrameworkCore;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Entities.Users;
using UserManagementApp.Domain.Interfaces.Repositories.Users;
using UserManagementApp.Infrastructure.Context;

namespace UserManagementApp.Infrastructure.Repositories.Users;

public class UserRepository : Repository<User> , IUserRepository
{
    private readonly UserManagementAppDbContext _context;
    public UserRepository(UserManagementAppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}


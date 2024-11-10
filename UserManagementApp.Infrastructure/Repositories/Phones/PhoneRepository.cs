using Microsoft.EntityFrameworkCore;
using UserManagementApp.Domain.Entities.Phones;
using UserManagementApp.Domain.Interfaces.Repositories.Phones;
using UserManagementApp.Infrastructure.Context;

namespace UserManagementApp.Infrastructure.Repositories.Phones;

public class PhoneRepository : Repository<Phone> , IPhoneRepository
{
    private readonly UserManagementAppDbContext _context;
    public PhoneRepository(UserManagementAppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Phone>> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Phones.Where(x => x.UserId == userId).ToListAsync();
    }
}


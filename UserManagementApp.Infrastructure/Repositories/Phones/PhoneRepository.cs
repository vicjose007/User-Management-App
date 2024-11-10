using Microsoft.EntityFrameworkCore;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Phones.Services.Projections;
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
    public async Task<IQueryable<Phone>> GetByUserId(string userId, CancellationToken cancellationToken = default)
    {
        return _context.Phones
            .Include(x => x.User)
            .Where(x => x.UserId == userId && x.User != null);
    }
}


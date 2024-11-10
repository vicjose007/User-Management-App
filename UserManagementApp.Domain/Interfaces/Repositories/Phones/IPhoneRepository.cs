using UserManagementApp.Domain.Entities.Phones;

namespace UserManagementApp.Domain.Interfaces.Repositories.Phones;

public interface IPhoneRepository : IRepository<Phone>
{
    Task<IQueryable<Phone>> GetByUserId(string userId, CancellationToken cancellationToken = default);
}

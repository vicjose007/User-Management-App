using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Dtos;

namespace UserManagementApp.Application.Users.Interfaces;

public interface IPhoneService
{
    Task<List<GetPhone>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GetPhone> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(CreatePhone create, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(string id, UpdatePhone create, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<List<GetPhone>> GetByUserId(string userId, CancellationToken cancellationToken = default);
}

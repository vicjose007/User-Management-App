using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Domain.Entities.Users;

namespace UserManagementApp.Application.Users.Interfaces;

public interface IUserService
{
    Task<List<GetUser>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken = default);
    Task<GetUser> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(CreateUser create, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(string id, UpdateUser create, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<string> ForgotPasswordAsync(string email);
    Task<int> UpdateUserActivationAsync(string id, bool isActive, CancellationToken cancellationToken = default);
    Task AddUserPhonesAsync(string userId, List<CreatePhone> dtos, CancellationToken cancellationToken = default);
    Task ValidateUserExists(string email, CancellationToken cancellationToken = default);
}

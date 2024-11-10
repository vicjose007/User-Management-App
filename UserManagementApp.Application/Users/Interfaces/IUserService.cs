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
    Task ForgotPasswordAsync(string email);
    Task<int> UpdateUserActivationAsync(string id, bool isActive, CancellationToken cancellationToken = default);
}

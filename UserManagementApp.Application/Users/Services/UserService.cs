using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Application.Users.Interfaces;
using UserManagementApp.Application.Users.Services.Projections;
using UserManagementApp.Domain.Enums;
using UserManagementApp.Domain.Interfaces.Repositories.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserManagementApp.Application.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IPhoneService _phoneService;

    public UserService(IUserRepository repository, IConfiguration configuration, IPhoneService phoneService)
    { 
        _repository = repository;
        _configuration = configuration;
        _phoneService = phoneService;
    }

    public async Task<List<GetUser>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.Queryable(cancellationToken).AsNoTracking()
            .Select(UserProjection.GetAll)
            .ToListAsync(cancellationToken);
    }

    public async Task<GetUser> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _repository.Queryable(cancellationToken)
            .AsNoTracking()
            .Where(st => st.Id == id)
            .Select(UserProjection.GetAll)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"Usuario no encontrado con este id: {id}");
    }

    public async Task<GetUser> GetByUserEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _repository.Queryable(cancellationToken)
            .AsNoTracking()
            .Where(st => st.Email == email)
            .Select(UserProjection.GetAll)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"Usuario no encontrado con este correo: {email}");
    }

    public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
  
        var userFind = await _repository.Queryable().Where(u => u.Email == request.Email && u.Password == request.Password).Select(UserProjection.GetAll).FirstOrDefaultAsync();

        if (userFind == null)
            throw new Exception("User is not found");


        string token = CreateToken(userFind);

        LoginResponse response = new()
        {
            Id = userFind.Id,
            Token = token,
            Created = userFind.Created,
            Modified = userFind.Updated,
            Last_login = userFind.LastLogin,
            IsActive = userFind.IsActive,
        };

        await UpdateLastLoginDate(userFind.Id);

        return response;

    }

    public async Task<int> AddAsync(CreateUser create, CancellationToken cancellationToken = default)
    {
        await ValidateUserExists(create.Email);

        create.Id = Guid.NewGuid().ToString();

        create.Role = Roles.Normal;

        create.IsActive = true;
        await _repository.AddAsync(create, cancellationToken);

        if (create.Phones != null && create.Phones.Any())
            await AddUserPhonesAsync(create.Id, create.Phones);

        return await _repository.SaveChangesAsync(cancellationToken);

    }

    public async Task<int> UpdateAsync(string id, UpdateUser update, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id);

        if (!string.IsNullOrEmpty(update.Name))
            user.Name = update.Name;

        if (!string.IsNullOrEmpty(update.Email))
            user.Email = update.Email;

        user.Updated = DateTimeOffset.Now;

        await _repository.UpdateAsync(user, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateUserPasswordAsync(string id, string password, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id);

        user.Password = password;

        await _repository.UpdateAsync(user, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateUserActivationAsync(string id, bool isActive, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id);

        user.IsActive = isActive;

        await _repository.UpdateAsync(user, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var User = await _repository.GetByIdAsync(id);

        await _repository.DeleteAsync(User, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task AddUserPhonesAsync(string userId, List<CreatePhone> dtos, CancellationToken cancellationToken = default)
    {
        foreach (var phone in dtos)
        {
            phone.UserId = userId;

            await _phoneService.AddAsync(phone, cancellationToken);
        }
    }

    public async Task ValidateUserExists(string email, CancellationToken cancellationToken = default)
    {
        var userExists = await _repository.Queryable().AnyAsync(u => u.Email == email);

        if (userExists)
            throw new Exception("El correo ya esta registrado");
    }

    private async Task<int> UpdateLastLoginDate(string id, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            throw new Exception("Este usuario no existe");

        user.LastLogin = DateTime.Now;

        await _repository.UpdateAsync(user, cancellationToken);

        return await _repository.SaveChangesAsync(cancellationToken);
    }

    private string CreateToken(GetUser user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(

            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;

    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await GetByUserEmailAsync(email);

        var createUser = new CreateUser
        {
            Name = user.Name,
            Email = user.Email,
            Password = GenerateRandomPassword(user.Name, 8)
        };

        await UpdateUserPasswordAsync(user.Id, createUser.Password);
    }

    public static string GenerateRandomPassword(string name, int length)
    {
        string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        char[] password = new char[length];

        int nameLength = Math.Min(length - 5, name.Length);
        for (int i = 0; i < nameLength; i++)
        {
            password[i] = name[i];
        }

        Random random = new Random();
        for (int i = nameLength; i < nameLength + 5; i++)
        {
            password[i] = characters[random.Next(characters.Length)];
        }

        for (int i = nameLength + 5; i < length; i++)
        {
            password[i] = characters[random.Next(characters.Length - 10)];
        }

        return new string(password);
    }
}


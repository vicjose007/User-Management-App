using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Application.Users.Interfaces;
using UserManagementApp.Application.Users.Services.Projections;
using UserManagementApp.Domain.Interfaces.Repositories.Users;

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
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"No user found with id: {id}");
    }

    public async Task<GetUser> GetByUserEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _repository.Queryable(cancellationToken)
            .AsNoTracking()
            .Where(st => st.Email == email)
            .Select(UserProjection.GetAll)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"No user found with email: {email}");
    }

    public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
  
        var userFind = await _repository.Queryable().Where(u => u.Email == request.Email && u.Password == request.Password).Select(UserProjection.GetAll).FirstOrDefaultAsync();

        if (userFind == null)
            throw new Exception("User is not found");


        string token = CreateToken(userFind);

        LoginResponse response = new()
        {
            Token = token,
            User = userFind,
        };

        return response;

    }

    public async Task<int> AddAsync(CreateUser create, CancellationToken cancellationToken = default)
    {
        create.Id = Guid.NewGuid().ToString();

        create.Role = Domain.Enums.Roles.Normal;

        create.Password = GenerateRandomPassword(create.Name, 6);

        await _repository.AddAsync(create, cancellationToken);

        await AddUserPhonesAsync(create.Id, create.Phones);

        return await _repository.SaveChangesAsync(cancellationToken);

    }

    public async Task<int> UpdateAsync(string id, UpdateUser update, CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(id);

        if (!string.IsNullOrEmpty(update.Name))
            user.Name = update.Name;

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

    public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var User = await _repository.GetByIdAsync(id);

        await _repository.DeleteAsync(User, cancellationToken);

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

    private async Task AddUserPhonesAsync(string userId, List<CreatePhone> dtos, CancellationToken cancellationToken = default)
    {
        foreach (var phone in dtos)
        {
            phone.UserId = userId;

            await _phoneService.AddAsync(phone, cancellationToken);
        }
    }
}


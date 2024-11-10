using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Phones.Services;
using UserManagementApp.Application.Users.Dtos;
using UserManagementApp.Application.Users.Interfaces;
using UserManagementApp.Application.Users.Services;
using UserManagementApp.Domain.Entities.Users;
using UserManagementApp.Domain.Enums;
using UserManagementApp.Domain.Interfaces.Repositories.Users;
using UserManagementApp.Infrastructure.Repositories.Phones;
using UserManagementApp.Infrastructure.Repositories.Users;
using UserManagementApp.Tests.Mocks;

namespace UserManagementApp.Tests.UserUnitTests;

public class UserTests
{
    private readonly UserRepository _mockUserRepository;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly PhoneRepository _mockPhoneRepository;
    private readonly Mock<IPhoneService> _phoneServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public UserTests()
    {
        _mockUserRepository = UserMocks.GetUserRepository(MockBaseContext.Get());
        _userServiceMock = new Mock<IUserService>();
        _phoneServiceMock = new Mock<IPhoneService>();
        _mockPhoneRepository = PhoneMocks.GetPhoneRepository(MockBaseContext.Get());
        _configurationMock = new Mock<IConfiguration>();
    }

    [Fact]
    public async Task AddAsync_ShouldCreateUser_WhenDataIsValid()
    {
        // Arrange
        var createUser = new CreateUser
        {
            Name = "Test User",
            Email = "victor@example.com",
            Password = "Password123"
        };

        var handler = new UserService(_mockUserRepository, _configurationMock.Object, _phoneServiceMock.Object);

        // Act
        var result = await handler.AddAsync(createUser);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenUserAlreadyExists()
    {
        // Arrange
        var createUser = new CreateUser
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "Password123"
        };


        var handler = new UserService(_mockUserRepository, _configurationMock.Object, _phoneServiceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.AddAsync(createUser));
    }

    [Fact]
    public async Task AddAsync_ShouldAssignDefaultRoleAndActiveStatus()
    {
        // Arrange
        var createUser = new CreateUser
        {
            Name = "Test User",
            Email = "raysa@example.com",
            Password = "Password123"
        };

        var handler = new UserService(_mockUserRepository, _configurationMock.Object, _phoneServiceMock.Object);

        // Act
        var result = await handler.AddAsync(createUser);

        // Assert
        Assert.Equal(Roles.Normal, createUser.Role);
        Assert.True(createUser.IsActive);
    }
}


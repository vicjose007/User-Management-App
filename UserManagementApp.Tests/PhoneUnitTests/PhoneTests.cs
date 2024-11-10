using Moq;
using UserManagementApp.Application.Phones.Dtos;
using UserManagementApp.Application.Phones.Services;
using UserManagementApp.Application.Users.Interfaces;
using UserManagementApp.Domain.Entities.Phones;
using UserManagementApp.Infrastructure.Repositories.Phones;
using UserManagementApp.Tests.Mocks;

namespace UserManagementApp.Tests.PhoneUnitTests;

public class PhoneTests
{
    private readonly PhoneRepository _mockPhoneRepository;
    private readonly Mock<IPhoneService> _phoneServiceMock;

    public PhoneTests()
    {
        _mockPhoneRepository = PhoneMocks.GetPhoneRepository(MockBaseContext.Get());
        _phoneServiceMock = new Mock<IPhoneService>();
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsException_WhenPhoneNotFound()
    {
        // Arrange
        var nonExistentPhoneId = "000000000000000000";

        var handler = new PhoneService(_mockPhoneRepository);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.GetByIdAsync(nonExistentPhoneId));
    }

    [Fact]
    public async Task AddAsync_ThrowsException_WhenPhoneNumberIsEmpty()
    {
        // Arrange
        var createPhone = new CreatePhone
        {
            UserId = "some-user-id",
            Number = "", 
            ContryCode = "+1",
            CityCode = "123"
        };

        var handler = new PhoneService(_mockPhoneRepository);

        // Act
        await Assert.ThrowsAsync<Exception>(() => handler.AddAsync(createPhone));
    }

}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagementApp.Application.Phones.Services;
using UserManagementApp.Application.Users.Interfaces;
using UserManagementApp.Application.Users.Services;

namespace UserManagementApp.Application;
/// <summary>
/// Realiza todas las inyecciones de dependencia relacionada con dicha capa.
/// </summary>
public static class IoC
{
    /// <summary>
    /// Agrega todas las inyecciones de dependencia 
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        return service
            .AddScoped<IUserService, UserService>()
            .AddScoped<IPhoneService, PhoneService>();

    }
}

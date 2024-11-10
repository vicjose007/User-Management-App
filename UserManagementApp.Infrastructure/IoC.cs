using Microsoft.Extensions.DependencyInjection;
using UserManagementApp.Domain.Interfaces.Repositories.Phones;
using UserManagementApp.Domain.Interfaces.Repositories.Users;
using UserManagementApp.Infrastructure.Repositories.Users;

namespace UserManagementApp.Infrastructure;
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
    public static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        return service
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IPhoneRepository, PhoneRepository>();
    }
}
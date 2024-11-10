using Microsoft.EntityFrameworkCore;
using UserManagementApp.Infrastructure.Context;

namespace UserManagementApp.API.Config
{
    public static class DbConfig
    {

        public static IServiceCollection ConfigDbConnection(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<UserManagementAppDbContext>(options => options.UseSqlServer(connectionString));

            return services;

        }

    }
}

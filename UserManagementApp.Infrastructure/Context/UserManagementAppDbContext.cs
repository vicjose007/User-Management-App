using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Entities.Phones;
using UserManagementApp.Domain.Entities.Users;
using UserManagementApp.Infrastructure.Extensions;

namespace UserManagementApp.Infrastructure.Context;


public class UserManagementAppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Phone> Phones { get; set; }

    public UserManagementAppDbContext(DbContextOptions<UserManagementAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = Assembly.GetAssembly(typeof(UserManagementAppDbContext));

        if (assembly is not null)
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(type.ClrType))
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
        }

        base.OnModelCreating(modelBuilder);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}


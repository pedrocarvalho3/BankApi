using BankApi.Core;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;
using BankApi.Infrastructure.Repositories;
using BankApi.Infrastructure.Security;
using BankApi.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankApi.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(connectionString));

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerAccountUnitOfWork, CustomerAccountUnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}

using BankApi.Core.Interfaces.Repositories;
using BankApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankApi.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));

        services.AddScoped<IAccountRepository, AccountRepository>();
        
        return services;
    }
}

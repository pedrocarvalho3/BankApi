using BankApi.Application.UseCases;
using BankApi.Application.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankApi.Application.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDepositUseCase, DepositUseCase>();
        services.AddScoped<IGetAllAccountsUseCase, GetAllAccountsUseCase>();
        services.AddScoped<IRegisterCustomerUseCase, RegisterCustomerUseCase>();
        services.AddScoped<IWithdrawUseCase, WithdrawUseCase>();

        return services;
    }
}

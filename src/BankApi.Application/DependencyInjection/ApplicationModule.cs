using BankApi.Application.UseCases;
using BankApi.Application.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankApi.Application.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticateUserUseCase, AuthenticateUserUseCase>();
        services.AddScoped<ICreateDepositUseCase, CreateDepositUseCase>();
        services.AddScoped<IGetAllAccountsUseCase, GetAllAccountsUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ICreateTransferUseCase, CreateTransferUseCase>();
        services.AddScoped<ICreateWithdrawUseCase, CreateWithdrawUseCase>();

        return services;
    }
}

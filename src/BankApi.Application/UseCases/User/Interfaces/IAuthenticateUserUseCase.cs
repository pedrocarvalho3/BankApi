using BankApi.Application.Contracts;

namespace BankApi.Application.UseCases.Interfaces;

public interface IAuthenticateUserUseCase
{
    Task<string> ExecuteAsync(AuthenticateUserRequest request);
}
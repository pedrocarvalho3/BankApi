using BankApi.Application.Contracts;

namespace BankApi.Application.UseCases.Interfaces;

public interface IAuthenticateCustomerUseCase
{
    Task<string> ExecuteAsync(AuthenticateCustomerRequest request);
}
using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface ICreateAccountUseCase
{
    Task<Account> ExecuteAsync(CreateAccountRequest request);
}

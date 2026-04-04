using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface ICreateAccountUseCase
{
    Account Execute(CreateAccountRequest request);
}

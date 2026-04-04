using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IGetAllAccountsUseCase
{
    List<Account> Execute();
}

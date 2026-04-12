using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IGetAllAccountsUseCase
{
    Task<List<Account>> ExecuteAsync();
}

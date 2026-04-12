using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Application.UseCases;

public class GetAllAccountsUseCase : IGetAllAccountsUseCase
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<Account>> ExecuteAsync()
    {
        return await _accountRepository.GetAllAsync();
    }
}

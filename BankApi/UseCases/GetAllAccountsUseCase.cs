using Bank.Models;
using BankApi.Repositories;

namespace BankApi.UseCases;

public class GetAllAccountsUseCase
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public List<Account> Execute()
    {
        return _accountRepository.GetAll();
    }
}
using Bank.Models;
using BankApi.Contracts;
using BankApi.Repositories;

namespace BankApi.UseCases;

public class CreateAccountUseCase
{
    private readonly IAccountRepository _accountRepository;
    
    public CreateAccountUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account Execute(CreateAccountRequest request)
    {
        var account = new Account(holder: request.Holder, accountType: request.AccountType);
        
        _accountRepository.Add(account);
        _accountRepository.SaveChanges();
        return account;
    }
}
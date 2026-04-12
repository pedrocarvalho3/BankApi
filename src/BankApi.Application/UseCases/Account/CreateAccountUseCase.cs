using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Application.UseCases;

public class CreateAccountUseCase : ICreateAccountUseCase
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> ExecuteAsync(CreateAccountRequest request)
    {
        var account = new Account(ownerId: request.OwnerId, accountType: request.AccountType);

        await _accountRepository.AddAsync(account);
        await _accountRepository.SaveChangesAsync();
        return account;
    }
}

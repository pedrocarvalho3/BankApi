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

    public Account Execute(CreateAccountRequest request)
    {
        var account = new Account(ownerId: request.OwnerId, accountType: request.AccountType);

        _accountRepository.Add(account);
        _accountRepository.SaveChanges();
        return account;
    }
}

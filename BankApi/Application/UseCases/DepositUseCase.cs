using Bank.Models;
using BankApi.Contracts;
using BankApi.Repositories;

namespace BankApi.UseCases;

public class DepositUseCase
{
    private readonly IAccountRepository _accountRepository;
    
    public DepositUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account Execute(CreateInternalTransactionRequest request)
    {
        var account = _accountRepository.GetById(request.accountId);
        
        if (account is null)
            throw new ArgumentException($"Account with id {request.accountId} does not exist");
        
        account.Deposit(request.amount);
        _accountRepository.SaveChanges();
        
        return account;
    }
}
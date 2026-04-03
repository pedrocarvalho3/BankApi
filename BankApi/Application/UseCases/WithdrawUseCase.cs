using Bank.Models;
using BankApi.Contracts;
using BankApi.Repositories;

namespace BankApi.UseCases;

public class WithdrawUseCase
{
    private readonly IAccountRepository _accountRepository;

    public WithdrawUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account Execute(CreateInternalTransactionRequest request)
    {
        var account = _accountRepository.GetById(request.accountId);

        if (account is null)
            throw new ArgumentException($"Account with id {request.accountId} does not exist");

        account.Withdraw(request.amount);

        _accountRepository.SaveChanges();

        return account;
    }
}
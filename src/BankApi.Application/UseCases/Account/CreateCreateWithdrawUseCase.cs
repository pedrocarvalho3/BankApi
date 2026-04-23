using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Application.UseCases;

public class CreateCreateWithdrawUseCase : ICreateWithdrawUseCase
{
    private readonly IAccountRepository _accountRepository;

    public CreateCreateWithdrawUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> ExecuteAsync(CreateInternalTransactionRequest request)
    {
        var account = await _accountRepository.GetByIdAsync(request.accountId);

        if (account is null)
            throw new ArgumentException($"Account with id {request.accountId} does not exist");

        account.Withdraw(request.amount);

        await _accountRepository.SaveChangesAsync();

        return account;
    }
}

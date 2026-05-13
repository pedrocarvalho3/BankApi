using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Application.UseCases;

public class CreateDepositUseCase : ICreateDepositUseCase
{
    private readonly IAccountTransactionUnitOfWork _unitOfWork;

    public CreateDepositUseCase(IAccountTransactionUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Transaction> ExecuteAsync(decimal amount, Guid accountId)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);

        if (account is null)
            throw new ArgumentException($"Account with id {accountId} does not exist");

        account.Deposit(amount);
        
        var transaction = Transaction.CreateDeposit(
            accountId,
            amount,
            account.Balance
        );
        
        await _unitOfWork.Transactions.AddAsync(transaction);
        
        await _unitOfWork.SaveChangesAsync();

        return transaction;
    }
}

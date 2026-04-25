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

    public async Task<Transaction> ExecuteAsync(CreateInternalTransactionRequest request)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(request.accountId);

        if (account is null)
            throw new ArgumentException($"Account with id {request.accountId} does not exist");

        account.Deposit(request.amount);
        
        var transaction = Transaction.CreateDeposit(
            request.accountId,
            request.amount,
            account.Balance
        );
        
        await _unitOfWork.Transactions.AddAsync(transaction);
        
        await _unitOfWork.SaveChangesAsync();

        return transaction;
    }
}

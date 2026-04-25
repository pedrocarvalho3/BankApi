using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Application.UseCases;

public class CreateWithdrawUseCase : ICreateWithdrawUseCase
{
    private readonly IAccountTransactionUnitOfWork _unitOfWork;

    public CreateWithdrawUseCase(IAccountTransactionUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Transaction> ExecuteAsync(CreateInternalTransactionRequest request)
    {
        var account = await _unitOfWork.Accounts.GetByIdAsync(request.accountId);

        if (account is null)
            throw new ArgumentException($"Account with id {request.accountId} does not exist");

        account.Withdraw(request.amount);

        var transaction = Transaction.CreateWithdraw(
            request.accountId,
            request.amount,
            account.Balance
        );
        
        await _unitOfWork.Transactions.AddAsync(transaction);
        
        await _unitOfWork.SaveChangesAsync();

        return transaction;
    }
}

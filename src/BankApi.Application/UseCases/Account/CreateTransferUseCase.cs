using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Application.UseCases;

public class CreateTransferUseCase : ICreateTransferUseCase
{
    private readonly IAccountTransactionUnitOfWork _unitOfWork;

    public CreateTransferUseCase(IAccountTransactionUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateTransferResponse> ExecuteAsync(CreateTransferRequest request)
    {
        if (request.SourceAccountId == request.DestinationAccountId)
            throw new ArgumentException("Source and destination accounts must be different.");

        var sourceAccount = await _unitOfWork.Accounts.GetByIdAsync(request.SourceAccountId);
        if (sourceAccount is null)
            throw new ArgumentException($"Account with id {request.SourceAccountId} does not exist");

        var destinationAccount = await _unitOfWork.Accounts.GetByIdAsync(request.DestinationAccountId);
        if (destinationAccount is null)
            throw new ArgumentException($"Account with id {request.DestinationAccountId} does not exist");

        sourceAccount.Withdraw(request.Amount);
        destinationAccount.Deposit(request.Amount);

        var transferId = Guid.NewGuid();

        var debitTransaction = Transaction.CreateTransferDebit(
            request.SourceAccountId,
            request.Amount,
            sourceAccount.Balance,
            transferId
        );

        var creditTransaction = Transaction.CreateTransferCredit(
            request.DestinationAccountId,
            request.Amount,
            destinationAccount.Balance,
            transferId
        );

        await _unitOfWork.Transactions.AddAsync(debitTransaction);
        await _unitOfWork.Transactions.AddAsync(creditTransaction);

        await _unitOfWork.SaveChangesAsync();

        return new CreateTransferResponse(
            transferId,
            request.SourceAccountId,
            request.DestinationAccountId,
            request.Amount
        );
    }
}

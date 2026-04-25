using BankApi.Core.Enums;

namespace BankApi.Core.Entities;

public class Transaction
{
    private Transaction(
        Guid accountId,
        ETransactionType transactionType,
        decimal amount,
        decimal balanceAfter,
        Guid? transferId = null)
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException("AccountId is required.", nameof(accountId));

        if (amount <= 0)
            throw new InvalidOperationException("Amount must be greater than zero.");

        Id = Guid.NewGuid();
        AccountId = accountId;
        TransactionType = transactionType;
        Amount = amount;
        BalanceAfter = balanceAfter;
        TransferId = transferId;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Transaction()
    {}
    
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public ETransactionType TransactionType { get; private set; }
    public decimal Amount { get; private set; }
    public decimal BalanceAfter { get; private set; }
    
    public Guid? TransferId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Transaction CreateDeposit(
        Guid accountId,
        decimal amount,
        decimal balanceAfter)
    {
        return new Transaction(
            accountId,
            ETransactionType.Deposit,
            amount,
            balanceAfter
        );
    }

    public static Transaction CreateWithdraw(
        Guid accountId,
        decimal amount,
        decimal balanceAfter)
    {
        return new Transaction(
            accountId,
            ETransactionType.Withdraw,
            amount,
            balanceAfter
        );
    }

    public static Transaction CreateTransferDebit(
        Guid accountId,
        decimal amount,
        decimal balanceAfter,
        Guid transferId)
    {
        return new Transaction(
            accountId,
            ETransactionType.TransferDebit,
            amount,
            balanceAfter,
            transferId
        );
    }

    public static Transaction CreateTransferCredit(
        Guid accountId,
        decimal amount,
        decimal balanceAfter,
        Guid transferId)
    {
        return new Transaction(
            accountId,
            ETransactionType.TransferCredit,
            amount,
            balanceAfter,
            transferId
        );
    }
}
namespace BankApi.Contracts;

public record CreateInternalTransactionRequest(int accountId, decimal amount);
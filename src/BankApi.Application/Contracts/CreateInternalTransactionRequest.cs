namespace BankApi.Application.Contracts;

public record CreateInternalTransactionRequest(int accountId, decimal amount);

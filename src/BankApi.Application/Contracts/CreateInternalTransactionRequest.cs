namespace BankApi.Application.Contracts;

public record CreateInternalTransactionRequest(Guid accountId, decimal amount);

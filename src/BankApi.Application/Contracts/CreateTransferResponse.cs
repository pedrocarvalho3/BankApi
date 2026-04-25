namespace BankApi.Application.Contracts;

public record CreateTransferResponse(
    Guid TransferId,
    Guid SourceAccountId,
    Guid DestinationAccountId,
    decimal Amount
);

using BankApi.Core.Enums;

namespace BankApi.Application.Contracts;

public record CreateAccountRequest(Guid OwnerId, EAccountType AccountType);

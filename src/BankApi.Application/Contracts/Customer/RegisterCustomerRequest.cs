using BankApi.Core.Enums;

namespace BankApi.Application.Contracts;

public record RegisterCustomerRequest(
    string FullName,
    string Email,
    string Document,
    DateOnly BirthDate,
    string Phone,
    string Password,
    EAccountType AccountType
);

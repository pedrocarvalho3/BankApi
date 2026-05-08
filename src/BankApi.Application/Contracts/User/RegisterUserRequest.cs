using BankApi.Core.Enums;

namespace BankApi.Application.Contracts;

public record RegisterUserRequest(
    string FullName,
    string Email,
    string Document,
    DateOnly BirthDate,
    string Phone,
    string Password,
    EAccountType AccountType
);

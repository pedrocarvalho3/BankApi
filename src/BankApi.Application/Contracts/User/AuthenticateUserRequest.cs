namespace BankApi.Application.Contracts;

public record AuthenticateUserRequest(
    string Email,
    string Password
    );
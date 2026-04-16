namespace BankApi.Application.Contracts;

public record AuthenticateCustomerRequest(
    string Email,
    string Password
    );
using BankApi.Core.Enums;

namespace BankApi.Application.Contracts;

public record CreateAccountRequest(string Holder, EAccountType AccountType);

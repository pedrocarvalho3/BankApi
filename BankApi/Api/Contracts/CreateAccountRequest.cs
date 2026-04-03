using Bank.Models.Enums;

namespace BankApi.Contracts;

public record CreateAccountRequest(string Holder, AccountType AccountType);
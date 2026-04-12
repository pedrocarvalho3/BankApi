using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IDepositUseCase
{
    Task<Account> ExecuteAsync(CreateInternalTransactionRequest request);
}

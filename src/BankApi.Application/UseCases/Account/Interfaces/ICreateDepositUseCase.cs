using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface ICreateDepositUseCase
{
    Task<Account> ExecuteAsync(CreateInternalTransactionRequest request);
}

using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface ICreateDepositUseCase
{
    Task<Transaction> ExecuteAsync(decimal amount, Guid accountId);
}

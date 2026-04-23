using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface ICreateWithdrawUseCase
{
    Task<Account> ExecuteAsync(CreateInternalTransactionRequest request);
}

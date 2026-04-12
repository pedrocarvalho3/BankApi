using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IWithdrawUseCase
{
    Task<Account> ExecuteAsync(CreateInternalTransactionRequest request);
}

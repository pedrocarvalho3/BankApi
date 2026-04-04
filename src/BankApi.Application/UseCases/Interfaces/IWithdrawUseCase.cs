using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IWithdrawUseCase
{
    Account Execute(CreateInternalTransactionRequest request);
}

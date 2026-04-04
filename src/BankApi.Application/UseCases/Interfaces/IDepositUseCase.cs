using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IDepositUseCase
{
    Account Execute(CreateInternalTransactionRequest request);
}

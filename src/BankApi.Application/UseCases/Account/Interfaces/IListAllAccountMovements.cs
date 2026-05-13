using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IListAllAccountMovements
{
    Task<List<Transaction>> ExecuteAsync(Guid accountId);
}
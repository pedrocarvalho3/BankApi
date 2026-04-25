using BankApi.Core.Entities;

namespace BankApi.Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task SaveChangesAsync();
}
using BankApi.Core.Entities;

namespace BankApi.Core.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllByAccountIdAsync(Guid accountId);
    Task AddAsync(Transaction transaction);
    Task SaveChangesAsync();
}
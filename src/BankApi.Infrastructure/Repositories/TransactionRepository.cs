using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _dbContext;
    
    public async Task AddAsync(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
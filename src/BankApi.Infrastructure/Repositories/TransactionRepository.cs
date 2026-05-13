using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _dbContext;
    
    public TransactionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Transaction>> GetAllByAccountIdAsync(Guid accountId)
    {
        return await _dbContext.Transactions
            .Where(x => x.AccountId == accountId)
            .ToListAsync();
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
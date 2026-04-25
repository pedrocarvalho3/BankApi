using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _dbContext;

    public AccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _dbContext.Accounts.ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == id);
    }

    public async Task AddAsync(Account account)
    {
        await _dbContext.Accounts.AddAsync(account);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

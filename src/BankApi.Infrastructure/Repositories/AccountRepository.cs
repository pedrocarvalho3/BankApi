using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Infrastructure;

namespace BankApi.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _dbContext;

    public AccountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Account> GetAll()
    {
        return _dbContext.Accounts.ToList();
    }

    public Account? GetById(Guid id)
    {
        return _dbContext.Accounts.FirstOrDefault(acc => acc.Id == id);
    }

    public void Add(Account account)
    {
        _dbContext.Accounts.Add(account);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}

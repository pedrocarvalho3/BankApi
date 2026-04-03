using Bank.Models;
using BankApi.Infrastructure;

namespace BankApi.Repositories;

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

    public Account? GetById(int id)
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
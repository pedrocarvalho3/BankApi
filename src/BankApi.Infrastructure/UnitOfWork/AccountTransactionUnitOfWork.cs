using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Infrastructure.UnitOfWork;

public class AccountTransactionUnitOfWork: IAccountTransactionUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public AccountTransactionUnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _dbContext = dbContext;
        Accounts = accountRepository;
        Transactions = transactionRepository;
    }
    
    public IAccountRepository Accounts { get; }
    public ITransactionRepository Transactions { get; }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
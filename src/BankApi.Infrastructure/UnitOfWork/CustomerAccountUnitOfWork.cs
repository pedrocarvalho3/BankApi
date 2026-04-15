using BankApi.Core;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Infrastructure.UnitOfWork;

public class CustomerAccountUnitOfWork : ICustomerAccountUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public CustomerAccountUnitOfWork(
        AppDbContext dbContext,
        ICustomerRepository customerRepository,
        IAccountRepository accountRepository,
        IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        Customers = customerRepository;
        Accounts = accountRepository;
        PasswordHasher = passwordHasher;
    }

    public ICustomerRepository Customers { get; }
    public IAccountRepository Accounts { get; }
    public IPasswordHasher PasswordHasher { get; }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

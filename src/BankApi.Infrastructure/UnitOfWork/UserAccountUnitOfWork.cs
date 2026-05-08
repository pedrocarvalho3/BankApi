using BankApi.Core;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Infrastructure.UnitOfWork;

public class UserAccountUnitOfWork : IUserAccountUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UserAccountUnitOfWork(
        AppDbContext dbContext,
        IUserRepository userRepository,
        IAccountRepository accountRepository,
        IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        Users = userRepository;
        Accounts = accountRepository;
        PasswordHasher = passwordHasher;
    }

    public IUserRepository Users { get; }
    public IAccountRepository Accounts { get; }
    public IPasswordHasher PasswordHasher { get; }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

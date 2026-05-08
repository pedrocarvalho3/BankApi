using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Core.Interfaces.UnitOfWork;

public interface IUserAccountUnitOfWork
{
    IUserRepository Users { get; }
    IAccountRepository Accounts { get; }
    IPasswordHasher PasswordHasher { get; }
    Task SaveChangesAsync();
}

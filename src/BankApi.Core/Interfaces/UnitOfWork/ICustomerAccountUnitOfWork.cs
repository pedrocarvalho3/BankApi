using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Core.Interfaces.UnitOfWork;

public interface ICustomerAccountUnitOfWork
{
    ICustomerRepository Customers { get; }
    IAccountRepository Accounts { get; }
    IPasswordHasher PasswordHasher { get; }
    Task SaveChangesAsync();
}

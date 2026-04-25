using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Core.Interfaces.UnitOfWork;

public interface IAccountTransactionUnitOfWork
{
    IAccountRepository Accounts { get; }
    
    Task SaveChangesAsync();
}
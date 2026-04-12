using BankApi.Core.Entities;

namespace BankApi.Core.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(Guid id);
    Task AddAsync(Account account);
    Task SaveChangesAsync();
}

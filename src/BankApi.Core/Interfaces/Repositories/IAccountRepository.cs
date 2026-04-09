using BankApi.Core.Entities;

namespace BankApi.Core.Interfaces.Repositories;

public interface IAccountRepository
{
    List<Account> GetAll();
    Account? GetById(Guid id);
    void Add(Account account);
    void SaveChanges();
}
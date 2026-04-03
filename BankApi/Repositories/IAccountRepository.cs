using Bank.Models;

namespace BankApi.Repositories;

public interface IAccountRepository
{
    List<Account> GetAll();
    Account? GetById(int id);
    void Add(Account account);
    void SaveChanges();
}
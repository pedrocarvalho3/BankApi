using BankApi.Core.Entities;

namespace BankApi.Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByEmailAsync(string email);
    Task<Customer?> GetByDocumentAsync(string document);
    Task AddAsync(Customer customer);
    Task SaveChangesAsync();
}

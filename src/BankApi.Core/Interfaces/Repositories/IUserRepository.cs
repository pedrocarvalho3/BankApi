using BankApi.Core.Entities;

namespace BankApi.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByDocumentAsync(string document);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}

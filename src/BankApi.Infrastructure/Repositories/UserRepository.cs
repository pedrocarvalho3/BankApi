using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<User?> GetByDocumentAsync(string document)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(c => c.Document == document);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

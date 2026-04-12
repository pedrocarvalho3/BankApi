using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetByDocumentAsync(string document)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Document == document);
    }

    public async Task AddAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

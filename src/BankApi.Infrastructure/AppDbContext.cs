using BankApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Infrastructure;

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
}
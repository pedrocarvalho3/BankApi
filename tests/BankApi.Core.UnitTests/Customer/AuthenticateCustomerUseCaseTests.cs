using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Core.UnitTests;

public class AuthenticateCustomerUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldAuthenticateCustomer()
    {
        var fakeCustomer = new Customer(
            "John Doe",
            "john@email.com",
            "12345678901",
            new DateOnly(1990, 1, 1),
            "+5511999999999",
            "hashed-secret"
        );

        var customerRepository = new FakeCustomerRepository
        {
            ExistingByEmail = fakeCustomer,
        };

        var passwordHasher = new FakePasswordHasher();
        
        var tokenGenerator = new FakeTokenGenerator();

        var useCase = new AuthenticateCustomerUseCase(customerRepository, passwordHasher, tokenGenerator);

        var request = new AuthenticateCustomerRequest("john@email.com", "secret");

        var result = await useCase.ExecuteAsync(request);
        
        Assert.Equal("fake-jwt-token", result);
    }
    
    private sealed class FakeCustomerRepository : ICustomerRepository
    {
        public Customer? ExistingByEmail { get; set; }
        public Customer? ExistingByDocument { get; set; }
        public Customer? AddedCustomer { get; private set; }

        public Task<Customer?> GetByEmailAsync(string email) => Task.FromResult(ExistingByEmail);
        public Task<Customer?> GetByDocumentAsync(string document) => Task.FromResult(ExistingByDocument);

        public Task AddAsync(Customer customer)
        {
            AddedCustomer = customer;
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
    
    private sealed class FakePasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return $"hashed-{password}";
        }

        public bool Verify(string password, string passwordHash)
        {
            return passwordHash == $"hashed-{password}";
        }
    }

    private sealed class FakeTokenGenerator : ITokenGenerator
    {
        public string Generate(Guid customerId, string email)
        {
            return "fake-jwt-token";
        }
    }
}

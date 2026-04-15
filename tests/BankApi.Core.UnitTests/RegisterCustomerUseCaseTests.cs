using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Core.UnitTests;

public class RegisterCustomerUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldRegisterCustomerAndCreateAccount_WhenEmailAndDocumentAreUnique()
    {
        var customerRepository = new FakeCustomerRepository();
        var accountRepository = new FakeAccountRepository();
        var unitOfWork = new FakeCustomerAccountUnitOfWork(customerRepository, accountRepository);
        var useCase = new RegisterCustomerUseCase(unitOfWork);
        var request = new RegisterCustomerRequest(
            FullName: "John Doe",
            Email: "john.doe@email.com",
            Document: "12345678901",
            BirthDate: new DateOnly(1990, 5, 10),
            Phone: "+5511999999999",
            Password: "secret",
            AccountType: EAccountType.Current
        );

        var customer = await useCase.ExecuteAsync(request);

        Assert.Equal(request.FullName, customer.FullName);
        Assert.Equal(request.Email, customer.Email);
        Assert.Equal(request.Document, customer.Document);
        Assert.NotNull(customerRepository.AddedCustomer);
        Assert.NotNull(accountRepository.AddedAccount);
        Assert.Equal(customer.Id, accountRepository.AddedAccount!.OwnerId);
        Assert.Equal("hashed-secret", customer.Password);
        Assert.True(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenEmailAlreadyExists()
    {
        var customerRepository = new FakeCustomerRepository
        {
            ExistingByEmail = new Customer(
                "Existing",
                "john.doe@email.com",
                "11111111111",
                new DateOnly(1991, 1, 1),
                "+5511000000000",
                "pwd"
            )
        };
        var unitOfWork = new FakeCustomerAccountUnitOfWork(customerRepository, new FakeAccountRepository());
        var useCase = new RegisterCustomerUseCase(unitOfWork);
        var request = new RegisterCustomerRequest(
            FullName: "John Doe",
            Email: "john.doe@email.com",
            Document: "12345678901",
            BirthDate: new DateOnly(1990, 5, 10),
            Phone: "+5511999999999",
            Password: "secret",
            AccountType: EAccountType.Current
        );

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));

        Assert.Equal("A customer with this email already exists.", ex.Message);
        Assert.False(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenDocumentAlreadyExists()
    {
        var customerRepository = new FakeCustomerRepository
        {
            ExistingByDocument = new Customer(
                "Existing",
                "existing@email.com",
                "12345678901",
                new DateOnly(1991, 1, 1),
                "+5511000000000",
                "pwd"
            )
        };
        var unitOfWork = new FakeCustomerAccountUnitOfWork(customerRepository, new FakeAccountRepository());
        var useCase = new RegisterCustomerUseCase(unitOfWork);
        var request = new RegisterCustomerRequest(
            FullName: "John Doe",
            Email: "john.doe@email.com",
            Document: "12345678901",
            BirthDate: new DateOnly(1990, 5, 10),
            Phone: "+5511999999999",
            Password: "secret",
            AccountType: EAccountType.Current
        );

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));

        Assert.Equal("A customer with this document already exists.", ex.Message);
        Assert.False(unitOfWork.SaveChangesCalled);
    }

    private sealed class FakeCustomerAccountUnitOfWork : ICustomerAccountUnitOfWork
    {
        public FakeCustomerAccountUnitOfWork(ICustomerRepository customers, IAccountRepository accounts)
        {
            Customers = customers;
            Accounts = accounts;
            PasswordHasher = new FakePasswordHasher();
        }

        public ICustomerRepository Customers { get; }
        public IAccountRepository Accounts { get; }
        public IPasswordHasher PasswordHasher { get; }
        public bool SaveChangesCalled { get; private set; }

        public Task SaveChangesAsync()
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }
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

    private sealed class FakeAccountRepository : IAccountRepository
    {
        public Account? AddedAccount { get; private set; }

        public Task<List<Account>> GetAllAsync() => Task.FromResult(new List<Account>());
        public Task<Account?> GetByIdAsync(Guid id) => Task.FromResult<Account?>(null);

        public Task AddAsync(Account account)
        {
            AddedAccount = account;
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
}

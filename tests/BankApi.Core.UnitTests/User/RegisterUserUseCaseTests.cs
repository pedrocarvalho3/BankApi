using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;
using BankAccount = BankApi.Core.Entities.Account;

namespace BankApi.Core.UnitTests;

public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldRegisterUserAndCreateAccount_WhenEmailAndDocumentAreUnique()
    {
        var userRepository = new FakeUserRepository();
        var accountRepository = new FakeAccountRepository();
        var unitOfWork = new FakeUserAccountUnitOfWork(userRepository, accountRepository);
        var useCase = new RegisterUserUseCase(unitOfWork);
        var request = new RegisterUserRequest(
            FullName: "John Doe",
            Email: "john.doe@email.com",
            Document: "12345678901",
            BirthDate: new DateOnly(1990, 5, 10),
            Phone: "+5511999999999",
            Password: "secret",
            AccountType: EAccountType.Current
        );

        var user = await useCase.ExecuteAsync(request);

        Assert.Equal(request.FullName, user.FullName);
        Assert.Equal(request.Email, user.Email);
        Assert.Equal(request.Document, user.Document);
        Assert.NotNull(userRepository.AddedUser);
        Assert.NotNull(accountRepository.AddedAccount);
        Assert.Equal(user.Id, accountRepository.AddedAccount!.OwnerId);
        Assert.Equal("hashed-secret", user.Password);
        Assert.True(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenEmailAlreadyExists()
    {
        var userRepository = new FakeUserRepository
        {
            ExistingByEmail = new User(
                "Existing",
                "john.doe@email.com",
                "11111111111",
                new DateOnly(1991, 1, 1),
                "+5511000000000",
                "pwd"
            )
        };
        var unitOfWork = new FakeUserAccountUnitOfWork(userRepository, new FakeAccountRepository());
        var useCase = new RegisterUserUseCase(unitOfWork);
        var request = new RegisterUserRequest(
            FullName: "John Doe",
            Email: "john.doe@email.com",
            Document: "12345678901",
            BirthDate: new DateOnly(1990, 5, 10),
            Phone: "+5511999999999",
            Password: "secret",
            AccountType: EAccountType.Current
        );

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));

        Assert.Equal("A user with this email already exists.", ex.Message);
        Assert.False(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenDocumentAlreadyExists()
    {
        var userRepository = new FakeUserRepository
        {
            ExistingByDocument = new User(
                "Existing",
                "existing@email.com",
                "12345678901",
                new DateOnly(1991, 1, 1),
                "+5511000000000",
                "pwd"
            )
        };
        var unitOfWork = new FakeUserAccountUnitOfWork(userRepository, new FakeAccountRepository());
        var useCase = new RegisterUserUseCase(unitOfWork);
        var request = new RegisterUserRequest(
            FullName: "John Doe",
            Email: "john.doe@email.com",
            Document: "12345678901",
            BirthDate: new DateOnly(1990, 5, 10),
            Phone: "+5511999999999",
            Password: "secret",
            AccountType: EAccountType.Current
        );

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));

        Assert.Equal("A user with this document already exists.", ex.Message);
        Assert.False(unitOfWork.SaveChangesCalled);
    }

    private sealed class FakeUserAccountUnitOfWork : IUserAccountUnitOfWork
    {
        public FakeUserAccountUnitOfWork(IUserRepository users, IAccountRepository accounts)
        {
            Users = users;
            Accounts = accounts;
            PasswordHasher = new FakePasswordHasher();
        }

        public IUserRepository Users { get; }
        public IAccountRepository Accounts { get; }
        public IPasswordHasher PasswordHasher { get; }
        public bool SaveChangesCalled { get; private set; }

        public Task SaveChangesAsync()
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }
    }

    private sealed class FakeUserRepository : IUserRepository
    {
        public User? ExistingByEmail { get; set; }
        public User? ExistingByDocument { get; set; }
        public User? AddedUser { get; private set; }

        public Task<User?> GetByEmailAsync(string email) => Task.FromResult(ExistingByEmail);
        public Task<User?> GetByDocumentAsync(string document) => Task.FromResult(ExistingByDocument);

        public Task AddAsync(User user)
        {
            AddedUser = user;
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;
    }

    private sealed class FakeAccountRepository : IAccountRepository
    {
        public BankAccount? AddedAccount { get; private set; }

        public Task<List<BankAccount>> GetAllAsync() => Task.FromResult(new List<BankAccount>());
        public Task<BankAccount?> GetByIdAsync(Guid id) => Task.FromResult<BankAccount?>(null);

        public Task AddAsync(BankAccount account)
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

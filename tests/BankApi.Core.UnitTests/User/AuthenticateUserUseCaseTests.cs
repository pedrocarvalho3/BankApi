using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;
using BankAccount = BankApi.Core.Entities.Account;

namespace BankApi.Core.UnitTests;

public class AuthenticateUserUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldAuthenticateUser()
    {
        var fakeUser = new User(
            "John Doe",
            "john@email.com",
            "12345678901",
            new DateOnly(1990, 1, 1),
            "+5511999999999",
            "hashed-secret"
        );

        var userRepository = new FakeUserRepository
        {
            ExistingByEmail = fakeUser,
        };
        
        var fakeAccount = new BankAccount(fakeUser.Id, EAccountType.Current);

        var accountRepository = new FakeAccountRepository
        {
            ExistingByOwnerId = fakeAccount
        };

        var passwordHasher = new FakePasswordHasher();
        
        var tokenGenerator = new FakeTokenGenerator();

        var useCase = new AuthenticateUserUseCase(userRepository, accountRepository, passwordHasher, tokenGenerator);

        var request = new AuthenticateUserRequest("john@email.com", "secret");

        var result = await useCase.ExecuteAsync(request);
        
        Assert.Equal("fake-jwt-token", result);
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
        public string Generate(Guid userId, Guid accountId, string email)
        {
            return "fake-jwt-token";
        }
    }
    
    private sealed class FakeAccountRepository : IAccountRepository
    {
        public BankAccount? ExistingById { get; set; }
        public BankAccount? ExistingByOwnerId { get; set; }

        public Task<List<BankAccount>> GetAllAsync() => Task.FromResult(new List<BankAccount>());
        public Task<BankAccount?> GetByIdAsync(Guid id) => Task.FromResult(ExistingById);
        public Task<BankAccount?> GetByOwnerIdAsync(Guid ownerId) => Task.FromResult(ExistingByOwnerId);
        public Task AddAsync(BankAccount account) => Task.CompletedTask;
        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}

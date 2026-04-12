using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Core.UnitTests;

public class CreateAccountUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateAccount()
    {
        var repository = new FakeAccountRepository();
        var useCase = new CreateAccountUseCase(repository);
        var request = new CreateAccountRequest(Guid.NewGuid(), EAccountType.Current);
        
        var account = await useCase.ExecuteAsync(request);
        
        Assert.Equal(request.OwnerId, account.OwnerId);
        Assert.Equal(request.AccountType, account.AccountType);
        Assert.True(repository.SaveChangesCalled);
        Assert.NotNull(repository.AddedAccount);
    }
    
    private sealed class FakeAccountRepository : IAccountRepository
    {
        public Account? ExistingById { get; set; }
        public Account? AddedAccount { get; set; }
        public bool SaveChangesCalled { get; private set; }
        
        public Task<List<Account>> GetAllAsync()
        {
            return Task.FromResult(new List<Account>());
        }

        public Task<Account?> GetByIdAsync(Guid id) => Task.FromResult(ExistingById);

        public Task AddAsync(Account account)
        {
            AddedAccount = account;
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }
    }
}

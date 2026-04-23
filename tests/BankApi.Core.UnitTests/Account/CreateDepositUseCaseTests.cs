using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;
using BankAccount = BankApi.Core.Entities.Account;

namespace BankApi.Core.UnitTests.Account;

public class CreateDepositUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateDeposit()
    {
        var ownerId = Guid.NewGuid();
        
        var fakeAccount = new BankAccount(
            ownerId,
            EAccountType.Current
        );

        var accountRepository = new FakeAccountRepository
        {
            ExistingById = fakeAccount
        };
        
        var useCase = new CreateDepositUseCase(accountRepository);

        var request = new CreateInternalTransactionRequest(ownerId, 100);
        
        var result = await useCase.ExecuteAsync(request);
        
        Assert.NotNull(result);
        Assert.Equal(EAccountType.Current, result.AccountType);
        Assert.Equal(ownerId, result.OwnerId);
        Assert.Equal(100, result.Balance);
    }
    
    private sealed class FakeAccountRepository : IAccountRepository
    {
        public BankAccount? AddedAccount { get; set; }
        public BankAccount? ExistingById { get; set; }
        public Task<List<BankAccount>> GetAllAsync() => Task.FromResult(new List<BankAccount>());
        public Task<BankAccount?> GetByIdAsync(Guid id) => Task.FromResult(ExistingById);

        public Task AddAsync(BankAccount account)
        {
            AddedAccount = account;
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}
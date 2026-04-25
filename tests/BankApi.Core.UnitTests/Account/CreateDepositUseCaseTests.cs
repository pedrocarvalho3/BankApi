using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;
using BankAccount = BankApi.Core.Entities.Account;

namespace BankApi.Core.UnitTests.Account;

public class CreateDepositUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateDepositTransaction()
    {
        var accountId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();

        var fakeAccount = new BankAccount(ownerId, EAccountType.Current);
        var accountRepository = new FakeAccountRepository { ExistingById = fakeAccount };
        var transactionRepository = new FakeTransactionRepository();
        var unitOfWork = new FakeAccountTransactionUnitOfWork(accountRepository, transactionRepository);
        var useCase = new CreateDepositUseCase(unitOfWork);

        var request = new CreateInternalTransactionRequest(accountId, 100);

        var result = await useCase.ExecuteAsync(request);

        Assert.NotNull(result);
        Assert.Equal(ETransactionType.Deposit, result.TransactionType);
        Assert.Equal(100, result.Amount);
        Assert.Same(result, transactionRepository.AddedTransaction);
        Assert.True(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowArgumentException_WhenAccountDoesNotExist()
    {
        var accountRepository = new FakeAccountRepository();
        var transactionRepository = new FakeTransactionRepository();
        var unitOfWork = new FakeAccountTransactionUnitOfWork(accountRepository, transactionRepository);
        var useCase = new CreateDepositUseCase(unitOfWork);

        var invalidAccountId = Guid.NewGuid();
        var request = new CreateInternalTransactionRequest(invalidAccountId, 100);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));

        Assert.Equal($"Account with id {invalidAccountId} does not exist", ex.Message);
        Assert.False(unitOfWork.SaveChangesCalled);
    }

    private sealed class FakeAccountTransactionUnitOfWork : IAccountTransactionUnitOfWork
    {
        public FakeAccountTransactionUnitOfWork(IAccountRepository accounts, ITransactionRepository transactions)
        {
            Accounts = accounts;
            Transactions = transactions;
        }

        public IAccountRepository Accounts { get; }
        public ITransactionRepository Transactions { get; }
        public bool SaveChangesCalled { get; private set; }

        public Task SaveChangesAsync()
        {
            SaveChangesCalled = true;
            return Task.CompletedTask;
        }
    }

    private sealed class FakeAccountRepository : IAccountRepository
    {
        public BankAccount? ExistingById { get; set; }

        public Task<List<BankAccount>> GetAllAsync() => Task.FromResult(new List<BankAccount>());
        public Task<BankAccount?> GetByIdAsync(Guid id) => Task.FromResult(ExistingById);
        public Task AddAsync(BankAccount account) => Task.CompletedTask;
        public Task SaveChangesAsync() => Task.CompletedTask;
    }

    private sealed class FakeTransactionRepository : ITransactionRepository
    {
        public Transaction? AddedTransaction { get; private set; }

        public Task AddAsync(Transaction transaction)
        {
            AddedTransaction = transaction;
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}

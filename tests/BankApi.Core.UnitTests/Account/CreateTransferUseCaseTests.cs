using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;
using BankApi.Core.Interfaces.UnitOfWork;
using BankAccount = BankApi.Core.Entities.Account;

namespace BankApi.Core.UnitTests.Account;

public class CreateTransferUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateTransferAndTransactions()
    {
        var sourceAccountId = Guid.NewGuid();
        var destinationAccountId = Guid.NewGuid();
        var sourceOwnerId = Guid.NewGuid();
        var destinationOwnerId = Guid.NewGuid();

        var sourceAccount = new BankAccount(sourceOwnerId, EAccountType.Current);
        sourceAccount.Deposit(400);
        var destinationAccount = new BankAccount(destinationOwnerId, EAccountType.Current);
        destinationAccount.Deposit(50);

        var accountRepository = new FakeAccountRepository
        {
            SourceAccount = sourceAccount,
            DestinationAccount = destinationAccount,
            SourceAccountId = sourceAccountId,
            DestinationAccountId = destinationAccountId
        };

        var transactionRepository = new FakeTransactionRepository();
        var unitOfWork = new FakeAccountTransactionUnitOfWork(accountRepository, transactionRepository);
        var useCase = new CreateTransferUseCase(unitOfWork);

        var request = new CreateTransferRequest(sourceAccountId, destinationAccountId, 100);

        var response = await useCase.ExecuteAsync(request);

        Assert.NotEqual(Guid.Empty, response.TransferId);
        Assert.Equal(sourceAccountId, response.SourceAccountId);
        Assert.Equal(destinationAccountId, response.DestinationAccountId);
        Assert.Equal(100, response.Amount);
        Assert.Equal(2, transactionRepository.AddedTransactions.Count);
        Assert.Contains(transactionRepository.AddedTransactions, t => t.TransactionType == ETransactionType.TransferDebit);
        Assert.Contains(transactionRepository.AddedTransactions, t => t.TransactionType == ETransactionType.TransferCredit);
        Assert.True(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenSourceAndDestinationAreSame()
    {
        var accountId = Guid.NewGuid();
        var accountRepository = new FakeAccountRepository();
        var transactionRepository = new FakeTransactionRepository();
        var unitOfWork = new FakeAccountTransactionUnitOfWork(accountRepository, transactionRepository);
        var useCase = new CreateTransferUseCase(unitOfWork);

        var request = new CreateTransferRequest(accountId, accountId, 100);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));

        Assert.Equal("Source and destination accounts must be different.", ex.Message);
        Assert.False(unitOfWork.SaveChangesCalled);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenDestinationAccountDoesNotExist()
    {
        var sourceAccountId = Guid.NewGuid();
        var destinationAccountId = Guid.NewGuid();
        var sourceOwnerId = Guid.NewGuid();

        var sourceAccount = new BankAccount(sourceOwnerId, EAccountType.Current);
        sourceAccount.Deposit(400);

        var accountRepository = new FakeAccountRepository
        {
            SourceAccount = sourceAccount,
            SourceAccountId = sourceAccountId,
            DestinationAccountId = destinationAccountId
        };
        var transactionRepository = new FakeTransactionRepository();
        var unitOfWork = new FakeAccountTransactionUnitOfWork(accountRepository, transactionRepository);
        var useCase = new CreateTransferUseCase(unitOfWork);

        var request = new CreateTransferRequest(sourceAccountId, destinationAccountId, 100);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));

        Assert.Equal($"Account with id {destinationAccountId} does not exist", ex.Message);
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
        public Guid SourceAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public BankAccount? SourceAccount { get; set; }
        public BankAccount? DestinationAccount { get; set; }

        public Task<List<BankAccount>> GetAllAsync() => Task.FromResult(new List<BankAccount>());

        public Task<BankAccount?> GetByIdAsync(Guid id)
        {
            if (id == SourceAccountId) return Task.FromResult(SourceAccount);
            if (id == DestinationAccountId) return Task.FromResult(DestinationAccount);
            return Task.FromResult<BankAccount?>(null);
        }

        public Task AddAsync(BankAccount account) => Task.CompletedTask;
        public Task SaveChangesAsync() => Task.CompletedTask;
    }

    private sealed class FakeTransactionRepository : ITransactionRepository
    {
        public List<Transaction> AddedTransactions { get; } = new();

        public Task AddAsync(Transaction transaction)
        {
            AddedTransactions.Add(transaction);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}

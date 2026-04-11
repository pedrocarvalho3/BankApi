using BankApi.Application.Contracts;
using BankApi.Application.UseCases;
using BankApi.Core.Entities;
using BankApi.Core.Enums;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Core.UnitTests;

public class CreateAccountUseCaseTests
{
    [Fact]
    public void Execute_ShouldCreateAccount()
    {
        var repository = new FakeAccountRepository();
        var useCase = new CreateAccountUseCase(repository);
        var request = new CreateAccountRequest(Guid.NewGuid(), EAccountType.Current);
        
        var account = useCase.Execute(request);
        
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
        public List<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account? GetById(Guid id) => ExistingById;

        public void Add(Account account) => AddedAccount = account;

        public void SaveChanges() => SaveChangesCalled = true;
    }
}
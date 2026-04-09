using BankApi.Core.Entities;
using BankApi.Core.Enums;

namespace BankApi.Core.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        var account = new Account(new Guid(), EAccountType.Current);

        account.Deposit(100);

        Assert.Equal(100, account.Balance);
    }
}

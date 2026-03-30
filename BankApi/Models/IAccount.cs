namespace Bank.Models;

public interface IAccount
{
    void Deposit(decimal value, bool isTransfer = false);
    void Withdraw(decimal value, bool isTransfer = false);
    void Transfer(Account destinationAccount, decimal value);
}
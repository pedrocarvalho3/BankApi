namespace Bank.Models;

public class CurrentAccount : Account
{
    private const decimal WithdrawFeeRate = 0.01m;
    private const decimal MaxWithdrawPercentage = 0.50m;
    public CurrentAccount(string holder) : base(holder) { }
    
    public override void Withdraw(decimal amount, bool isTransfer)
    {
        ValidatePositiveAmount(amount);
        ValidateSufficientBalance(amount);
        
        var maxAllowedPerOperation = MaxWithdrawPercentage * Balance;
        if (maxAllowedPerOperation < amount)
            throw new InvalidOperationException("The withdrawal exceeds the allowed per-operation limit.");
        
        var fee = WithdrawFeeRate * amount;
        var totalDebit = amount + fee;
        this.Balance -= totalDebit;

        if (!isTransfer)
            AddTransaction(
                TransactionType.Withdraw,
                amount,
                $"Withdrawal completed with fee of {fee:C}."
            );
    }
}
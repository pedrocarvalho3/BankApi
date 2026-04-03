// namespace Bank.Models;
//
// public class SavingsAccount : Account
// {
//     public SavingsAccount(string holder) : base(holder) { }
//     
//     public override void Withdraw(decimal amount, bool isTransfer)
//     {
//         ValidatePositiveAmount(amount);
//
//         ValidateSufficientBalance(amount);
//
//         this.Balance -= amount;
//         if (!isTransfer)
//             AddTransaction(TransactionType.Withdraw, amount: amount, "Withdraw completed.");
//     }
// }
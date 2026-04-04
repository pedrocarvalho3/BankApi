using BankApi.Core.Enums;

namespace BankApi.Core.Entities;

public class Account
    {
        public int Id { get; private set; }
        public string Holder { get; private set; }
        public decimal Balance { get; private set; }
        public EAccountType AccountType { get; private set; }
        // public List<Transaction> Transactions { get; private set; } = new();

        protected Account() { }

        public Account(string holder, EAccountType accountType)
        {
            if (string.IsNullOrWhiteSpace(holder))
                throw new ArgumentNullException(nameof(holder), "Holder is required");

            Holder = holder;
            AccountType = accountType;
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            ValidatePositiveAmount(amount);
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            ValidatePositiveAmount(amount);

            switch (AccountType)
            {
                case EAccountType.Savings:
                    WithdrawFromSavings(amount);
                    break;

                case EAccountType.Current:
                    WithdrawFromCurrent(amount);
                    break;

                default:
                    throw new InvalidOperationException("Invalid account type.");
            }
        }

        private void WithdrawFromSavings(decimal amount)
        {
            ValidateSufficientBalance(amount);
            Balance -= amount;
        }

        private void WithdrawFromCurrent(decimal amount)
        {
            const decimal withdrawFeeRate = 0.01m;
            const decimal maxWithdrawPercentage = 0.50m;

            var maxAllowedPerOperation = Balance * maxWithdrawPercentage;
            if (amount > maxAllowedPerOperation)
                throw new InvalidOperationException("The withdrawal exceeds the allowed per-operation limit.");

            var fee = amount * withdrawFeeRate;
            var totalDebit = amount + fee;

            ValidateSufficientBalance(totalDebit);
            Balance -= totalDebit;
        }

        private void ValidatePositiveAmount(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero.");
        }

        private void ValidateSufficientBalance(decimal amount)
        {
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");
        }
    }
   
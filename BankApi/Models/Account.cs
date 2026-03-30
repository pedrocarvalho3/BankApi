namespace Bank.Models
{
    public abstract class Account : IAccount
    {
        private readonly List<Transaction> _transaction = new();
        
        public string Holder { get; set; }
        public decimal Balance { get; protected set; }
        public IReadOnlyCollection<Transaction> Transactions => _transaction.AsReadOnly();

        public Account(string holder)
        {
            if (string.IsNullOrWhiteSpace(holder))
                throw new ArgumentNullException("Holder is required");
            
            this.Holder = holder;
            Balance = 0;
        }
        
        public abstract void Withdraw(decimal amount, bool isTransfer);

        public void Deposit(decimal amount, bool isTransfer)
        {
            ValidatePositiveAmount(amount);

            this.Balance += amount;

            if (!isTransfer)
                AddTransaction(TransactionType.Deposit, amount, "Deposit completed.");
        }
        
        public void Transfer(Account destinationAccount, decimal amount)
        {
            if (destinationAccount is null)
                throw new ArgumentNullException(nameof(destinationAccount));

            if (ReferenceEquals(this, destinationAccount))
                throw new InvalidOperationException("Cannot transfer to the same account.");
            
            ValidatePositiveAmount(amount);
            
            ValidateSufficientBalance(amount);
            
            Withdraw(amount, true);
            destinationAccount.Deposit(amount, true);
            
            AddTransaction(
                TransactionType.TransferSent,
                amount,
                $"Transfer sent to {destinationAccount.Holder}."
            );

            destinationAccount.AddTransaction(
                TransactionType.TransferReceived,
                amount,
                $"Transfer received from {Holder}."
            );
        }

        public override string ToString()
        {
            return $"Holder: {Holder}, Balance: {Balance}";
        }

        protected void ValidatePositiveAmount(decimal amount)
        {
            if (amount < 0)
                throw new InvalidOperationException("Amount must be positive.");
        }

        protected void ValidateSufficientBalance(decimal amount)
        {
            if (this.Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");
        }

        protected void AddTransaction(TransactionType type, decimal amount, string description)
        {
            _transaction.Add(new Transaction(type, amount, description));
        }

    }
}

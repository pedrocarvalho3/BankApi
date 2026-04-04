// namespace BankApi.Core.Entities;
//
// public class Transaction
// {
//     public int Id { get; private set; }
//     public TransactionType Type { get; set; }
//     public decimal Amount { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public string Description { get; set; }
//
//     public Transaction(TransactionType type, decimal amount, string description)
//     {
//         Type = type;
//         Amount = amount;
//         Description = description;
//         CreatedAt = DateTime.UtcNow;
//     }
//
//     public override string ToString()
//     {
//         return $"{CreatedAt:dd/MM/yyyy HH:mm:ss} - {Type} - Amount: {Amount:C} - {Description}";
//     }
// }
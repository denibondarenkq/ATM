namespace Application.Models.Transactions;

public record Transaction(string Username, TransactionType Type, decimal Amount);

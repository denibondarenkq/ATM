using Application.Models.Transactions;

namespace Application.Contracts.Transactions;

public interface ITransactionService
{
    public void RecordTransaction(string username, TransactionType type, decimal amount);
    public ViewTransactionResult ViewTransactionHistoryByCustomer(string username);
}
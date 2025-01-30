using Application.Models.Transactions;

namespace Application.Abstractions.Repositories;

public interface ITransactionRepository
{
    public Task RecordTransaction(string username, TransactionType type, decimal amount);
    public Task<IEnumerable<Transaction>> ViewTransactionHistoryByCustomer(string username);
}
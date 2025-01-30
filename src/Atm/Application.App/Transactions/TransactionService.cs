using Application.Abstractions.Repositories;
using Application.Contracts.Transactions;
using Application.Models.Transactions;

namespace Application.App.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public void RecordTransaction(string username, TransactionType type, decimal amount)
    {
        _transactionRepository.RecordTransaction(username, type, amount).Wait();
    }

    public ViewTransactionResult ViewTransactionHistoryByCustomer(string username)
    {
        IEnumerable<Transaction>? transactions = _transactionRepository.ViewTransactionHistoryByCustomer(username).Result;
        if (transactions == null || !transactions.Any())
        {
            return new ViewTransactionResult.Failure("No transactions found for user.");
        }

        return new ViewTransactionResult.Success(transactions);
    }
}

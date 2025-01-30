namespace Application.Models.Transactions;

public abstract record ViewTransactionResult
{
    private ViewTransactionResult() { }

    public record Success(IEnumerable<Transaction> Transactions) : ViewTransactionResult;
    public record Failure(string Reason) : ViewTransactionResult;
}
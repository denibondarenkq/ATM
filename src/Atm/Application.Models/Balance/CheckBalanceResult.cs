namespace Application.Models.Balance;

public abstract record CheckBalanceResult
{
    private CheckBalanceResult() { }

    public record Success(decimal Balance) : CheckBalanceResult;
    public record BalanceInformationNotFound(string Reason) : CheckBalanceResult;
}

namespace Application.Models.Balance;

public abstract record UpdateBalanceResult
{
    private UpdateBalanceResult() { }

    public record Success(decimal NewBalance) : UpdateBalanceResult;
    public record Failure(string Reason) : UpdateBalanceResult;
}

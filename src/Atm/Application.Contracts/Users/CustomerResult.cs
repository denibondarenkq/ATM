using Application.Models.Users;

namespace Application.Contracts.Users;

public abstract record CustomerResult
{
    public record Success(Customer Customer) : CustomerResult;
    public record NotFound : CustomerResult;
}
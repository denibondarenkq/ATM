using Application.Models.Users;

namespace Application.Contracts.Users;

public abstract record AdminResult
{
    public record Success(Admin Admin) : AdminResult;
    public record NotFound : AdminResult;
}
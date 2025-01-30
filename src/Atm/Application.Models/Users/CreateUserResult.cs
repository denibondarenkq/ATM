namespace Application.Models.Users;

public abstract record CreateUserResult
{
    private CreateUserResult() { }

    public record Success(string Username) : CreateUserResult;
    public record Failure(string Reason) : CreateUserResult;
}
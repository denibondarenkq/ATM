namespace Application.Contracts.Users;

public abstract record ChangePasswordResult
{
    public record Success : ChangePasswordResult;
    public record Failure(string Reason) : ChangePasswordResult;
}
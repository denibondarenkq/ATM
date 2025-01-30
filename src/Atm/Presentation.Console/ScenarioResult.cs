namespace Presentation.Console;

public abstract record ScenarioResult
{
    private ScenarioResult() { }

    public sealed record Success(string Message) : ScenarioResult;
    public sealed record Failure(string ErrorMessage) : ScenarioResult;
}
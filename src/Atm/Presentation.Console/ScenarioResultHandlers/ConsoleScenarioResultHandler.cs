using Spectre.Console;

namespace Presentation.Console.ScenarioResultHandlers;

public class ConsoleScenarioResultHandler : IScenarioResultHandler
{
    public void Handle(ScenarioResult result)
    {
        switch (result)
        {
            case ScenarioResult.Success success:
                AnsiConsole.MarkupLine($"[green]{success.Message}[/]");
                break;
            case ScenarioResult.Failure failure:
                AnsiConsole.MarkupLine($"[red]{failure.ErrorMessage}[/]");
                break;
        }
    }
}

using Presentation.Console.ScenarioResultHandlers;
using Spectre.Console;

namespace Presentation.Console;

public class ScenarioRunner
{
    private readonly IEnumerable<IScenarioProvider> _providers;

    public ScenarioRunner(IEnumerable<IScenarioProvider> providers)
    {
        _providers = providers;
    }

    public void Run()
    {
        IEnumerable<IScenario> scenarios = GetScenarios();

        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title("Select action:")
            .AddChoices(scenarios)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        ScenarioResult result = scenario.Run();

        var resultHandler = new ConsoleScenarioResultHandler();
        resultHandler.Handle(result);
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _providers)
        {
            if (provider.TryGetScenario(out IScenario? scenario))
            {
                yield return scenario;
            }
        }
    }
}
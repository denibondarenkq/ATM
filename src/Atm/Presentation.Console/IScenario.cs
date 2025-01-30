namespace Presentation.Console;

public interface IScenario
{
    string Name { get; }
    ScenarioResult Run();
}
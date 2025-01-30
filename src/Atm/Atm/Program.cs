using Application.App.Extensions;
using Infrastructure.DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Console;
using Presentation.Console.Extensions;

var collection = new ServiceCollection();

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 5432;
        configuration.Username = "username";
        configuration.Password = "password";
        configuration.Database = "atm";
        configuration.SslMode = "Prefer";
    })
    .AddConsolePresentation();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

scope.UseInfrastructureDataAccess();

ScenarioRunner scenarioRunner = scope.ServiceProvider
    .GetRequiredService<ScenarioRunner>();

while (true)
{
    scenarioRunner.Run();
}

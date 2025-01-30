using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenarios.CreateAdmin;
using Presentation.Console.Scenarios.CreateCustomer;
using Presentation.Console.Scenarios.DepositMoney;
using Presentation.Console.Scenarios.Login;
using Presentation.Console.Scenarios.ViewBalance;
using Presentation.Console.Scenarios.ViewTransactionHistory;
using Presentation.Console.Scenarios.WithdrawMoney;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsolePresentation(this IServiceCollection services)
    {
        services.AddScoped<ScenarioRunner>();
        services.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        services.AddScoped<IScenarioProvider, CreateAdminScenarioProvider>();
        services.AddScoped<IScenarioProvider, CreateCustomerScenarioProvider>();
        services.AddScoped<IScenarioProvider, ViewBalanceScenarioProvider>();
        services.AddScoped<IScenarioProvider, WithdrawMoneyScenarioProvider>();
        services.AddScoped<IScenarioProvider, DepositMoneyScenarioProvider>();
        services.AddScoped<IScenarioProvider, ViewTransactionHistoryScenarioProvider>();

        return services;
    }
}
using Application.Contracts.Users;
using Application.Models.Users;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CreateCustomer;

public class CreateCustomerScenario : IScenario
{
    private readonly IAdminService _adminService;
    private readonly ICurrentUserService _currentUserService;

    public CreateCustomerScenario(IAdminService adminService, ICurrentUserService currentUserService)
    {
        _adminService = adminService;
        _currentUserService = currentUserService;
    }

    public string Name => "Create Customer";

    public ScenarioResult Run()
    {
        if (_currentUserService.CurrentAdmin is null)
        {
            return new ScenarioResult.Failure("You are not authorized as an admin.");
        }

        string username = AnsiConsole.Ask<string>("Enter new customer username:");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter new customer password:").Secret());
        decimal initialBalance = AnsiConsole.Ask<decimal>("Enter initial balance:");

        CreateUserResult result = _adminService.CreateCustomer(username, password, initialBalance);
        return result switch
        {
            CreateUserResult.Success => new ScenarioResult.Success($"Customer {username} created successfully."),
            CreateUserResult.Failure failure => new ScenarioResult.Failure(failure.Reason),
            _ => new ScenarioResult.Failure("Unknown error occurred."),
        };
    }
}

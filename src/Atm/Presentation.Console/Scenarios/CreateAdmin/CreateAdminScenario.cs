using Application.Contracts.Users;
using Application.Models.Users;
using Spectre.Console;

namespace Presentation.Console.Scenarios.CreateAdmin;

public class CreateAdminScenario : IScenario
{
    private readonly IAdminService _adminService;
    private readonly ICurrentUserService _currentUserService;

    public CreateAdminScenario(IAdminService adminService, ICurrentUserService currentUserService)
    {
        _adminService = adminService;
        _currentUserService = currentUserService;
    }

    public string Name => "Create Admin";

    public ScenarioResult Run()
    {
        if (_currentUserService.CurrentAdmin is null)
        {
            return new ScenarioResult.Failure("You are not authorized as an admin.");
        }

        string username = AnsiConsole.Ask<string>("Enter new admin username:");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter new admin password:").Secret());

        CreateUserResult result = _adminService.CreateAdmin(username, password);
        return result switch
        {
            CreateUserResult.Success => new ScenarioResult.Success($"Admin {username} created successfully."),
            CreateUserResult.Failure failure => new ScenarioResult.Failure(failure.Reason),
            _ => new ScenarioResult.Failure("Unknown error occurred."),
        };
    }
}
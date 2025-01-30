using Application.Contracts.Users;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Login;

public class LoginScenario : IScenario
{
    private readonly IAdminService _adminService;
    private readonly ICustomerService _customerService;

    public LoginScenario(IAdminService adminService, ICustomerService customerService)
    {
        _adminService = adminService;
        _customerService = customerService;
    }

    public string Name => "Login";

    public ScenarioResult Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username:");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password:").Secret());

        LoginResult adminLoginResult = _adminService.Login(username, password);
        if (adminLoginResult is LoginResult.Success)
        {
            return new ScenarioResult.Success("Successfully logged in as an admin.");
        }

        LoginResult customerLoginResult = _customerService.Login(username, password);
        if (customerLoginResult is LoginResult.Success)
        {
            return new ScenarioResult.Success("Successfully logged in as a customer.");
        }

        return new ScenarioResult.Failure("Invalid username or password.");
    }
}
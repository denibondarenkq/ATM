using Application.Contracts.Users;
using Application.Models.Balance;

namespace Presentation.Console.Scenarios.ViewBalance;

public class ViewBalanceScenario : IScenario
{
    private readonly ICustomerService _customerService;
    private readonly ICurrentUserService _currentUserService;

    public ViewBalanceScenario(ICustomerService customerService, ICurrentUserService currentUserService)
    {
        _customerService = customerService;
        _currentUserService = currentUserService;
    }

    public string Name => "View Balance";

    public ScenarioResult Run()
    {
        if (_currentUserService.CurrentCustomer is null)
        {
            return new ScenarioResult.Failure("You are not authorized as a customer.");
        }

        string username = _currentUserService.CurrentCustomer.Username;
        CheckBalanceResult result = _customerService.CheckBalance(username);

        return result switch
        {
            CheckBalanceResult.Success success => new ScenarioResult.Success($"Your balance: {success.Balance}"),
            CheckBalanceResult.BalanceInformationNotFound failure => new ScenarioResult.Failure($"Failed to retrieve balance: {failure.Reason}"),
            _ => new ScenarioResult.Failure("Unknown error occurred."),
        };
    }
}
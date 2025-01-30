using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.ViewBalance;

public class ViewBalanceScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustomerService _customerService;

    public ViewBalanceScenarioProvider(ICurrentUserService currentUserService, ICustomerService customerService)
    {
        _currentUserService = currentUserService;
        _customerService = customerService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentCustomer != null)
        {
            scenario = new ViewBalanceScenario(_customerService, _currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}
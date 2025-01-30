using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.CreateCustomer;

public class CreateCustomerScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAdminService _adminService;

    public CreateCustomerScenarioProvider(ICurrentUserService currentUserService, IAdminService adminService)
    {
        _currentUserService = currentUserService;
        _adminService = adminService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAdmin != null)
        {
            scenario = new CreateCustomerScenario(_adminService, _currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}
using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.Login;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICustomerService _customerService;
    private readonly ICurrentUserService _currentUserService;

    public LoginScenarioProvider(IAdminService adminService, ICustomerService customerService, ICurrentUserService currentUserService)
    {
        _adminService = adminService;
        _customerService = customerService;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAdmin != null || _currentUserService.CurrentCustomer != null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginScenario(_adminService, _customerService);
        return true;
    }
}

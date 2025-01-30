using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.CreateAdmin;

public class CreateAdminScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAdminService _adminService;

    public CreateAdminScenarioProvider(ICurrentUserService currentUserService, IAdminService adminService)
    {
        _currentUserService = currentUserService;
        _adminService = adminService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentAdmin != null)
        {
            scenario = new CreateAdminScenario(_adminService, _currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}
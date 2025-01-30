using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Transactions;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.DepositMoney;

public class DepositMoneyScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustomerService _customerService;
    private readonly ITransactionService _transactionService;

    public DepositMoneyScenarioProvider(ICurrentUserService currentUserService, ICustomerService customerService, ITransactionService transactionService)
    {
        _currentUserService = currentUserService;
        _customerService = customerService;
        _transactionService = transactionService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentCustomer != null)
        {
            scenario = new DepositMoneyScenario(_customerService, _currentUserService, _transactionService);
            return true;
        }

        scenario = null;
        return false;
    }
}
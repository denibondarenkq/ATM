using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Transactions;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.WithdrawMoney;

public class WithdrawMoneyScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ICustomerService _customerService;
    private readonly ITransactionService _transactionService;

    public WithdrawMoneyScenarioProvider(ICurrentUserService currentUserService, ICustomerService customerService, ITransactionService transactionService)
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
            scenario = new WithdrawMoneyScenario(_currentUserService, _customerService, _transactionService);
            return true;
        }

        scenario = null;
        return false;
    }
}
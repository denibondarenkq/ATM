using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Transactions;
using Application.Contracts.Users;

namespace Presentation.Console.Scenarios.ViewTransactionHistory;

public class ViewTransactionHistoryScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ITransactionService _transactionService;

    public ViewTransactionHistoryScenarioProvider(ICurrentUserService currentUserService, ITransactionService transactionService)
    {
        _currentUserService = currentUserService;
        _transactionService = transactionService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.CurrentCustomer != null)
        {
            scenario = new ViewTransactionHistoryScenario(_transactionService, _currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}
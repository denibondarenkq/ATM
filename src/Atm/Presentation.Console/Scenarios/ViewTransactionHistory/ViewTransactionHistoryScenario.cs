using Application.Contracts.Transactions;
using Application.Contracts.Users;
using Application.Models.Transactions;

namespace Presentation.Console.Scenarios.ViewTransactionHistory;

public class ViewTransactionHistoryScenario : IScenario
{
    private readonly ITransactionService _transactionService;
    private readonly ICurrentUserService _currentUserService;

    public ViewTransactionHistoryScenario(ITransactionService transactionService, ICurrentUserService currentUserService)
    {
        _transactionService = transactionService;
        _currentUserService = currentUserService;
    }

    public string Name => "View Transaction History";

    public ScenarioResult Run()
    {
        if (_currentUserService.CurrentCustomer is null)
        {
            return new ScenarioResult.Failure("You are not authorized as a customer.");
        }

        string username = _currentUserService.CurrentCustomer.Username;
        ViewTransactionResult result = _transactionService.ViewTransactionHistoryByCustomer(username);

        return result switch
        {
            ViewTransactionResult.Success success => new ScenarioResult.Success($"Transaction history:\n{string.Join("\n", success.Transactions)}"),
            ViewTransactionResult.Failure failure => new ScenarioResult.Failure($"Failed to retrieve transaction history: {failure.Reason}"),
            _ => new ScenarioResult.Failure("Unknown error occurred."),
        };
    }
}
using Application.Contracts.Transactions;
using Application.Contracts.Users;
using Application.Models.Balance;
using Application.Models.Transactions;
using Spectre.Console;

namespace Presentation.Console.Scenarios.WithdrawMoney;

public class WithdrawMoneyScenario : IScenario
{
    private readonly ICustomerService _customerService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITransactionService _transactionService;

    public WithdrawMoneyScenario(ICurrentUserService currentUserService, ICustomerService customerService, ITransactionService transactionService)
    {
        _currentUserService = currentUserService;
        _customerService = customerService;
        _transactionService = transactionService;
    }

    public string Name => "Withdraw Money";

    public ScenarioResult Run()
    {
        if (_currentUserService.CurrentCustomer is null)
        {
            return new ScenarioResult.Failure("You are not authorized as a customer.");
        }

        string username = _currentUserService.CurrentCustomer.Username;
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to withdraw:");

        UpdateBalanceResult result = _customerService.WithdrawMoney(username, amount);
        _transactionService.RecordTransaction(username, TransactionType.Withdraw, amount);

        return result switch
        {
            UpdateBalanceResult.Success success => new ScenarioResult.Success($"Withdrawal successful. New balance: {success.NewBalance}"),
            UpdateBalanceResult.Failure failure => new ScenarioResult.Failure($"Failed to withdraw money: {failure.Reason}"),
            _ => new ScenarioResult.Failure("Unknown error occurred."),
        };
    }
}
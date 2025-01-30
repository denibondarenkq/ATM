using Application.Contracts.Transactions;
using Application.Contracts.Users;
using Application.Models.Balance;
using Application.Models.Transactions;
using Spectre.Console;

namespace Presentation.Console.Scenarios.DepositMoney;

public class DepositMoneyScenario : IScenario
{
    private readonly ICustomerService _customerService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITransactionService _transactionService;
    public DepositMoneyScenario(ICustomerService customerService, ICurrentUserService currentUserService, ITransactionService transactionService)
    {
        _customerService = customerService;
        _currentUserService = currentUserService;
        _transactionService = transactionService;
    }

    public string Name => "Deposit Money";

    public ScenarioResult Run()
    {
        if (_currentUserService.CurrentCustomer is null)
        {
            return new ScenarioResult.Failure("You are not authorized as a customer.");
        }

        string username = _currentUserService.CurrentCustomer.Username;
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to deposit:");

        UpdateBalanceResult result = _customerService.DepositMoney(username, amount);
        _transactionService.RecordTransaction(username, TransactionType.Deposit, amount);

        return result switch
        {
            UpdateBalanceResult.Success success => new ScenarioResult.Success($"Deposit successful. New balance: {success.NewBalance}"),
            UpdateBalanceResult.Failure failure => new ScenarioResult.Failure($"Failed to deposit money: {failure.Reason}"),
            _ => new ScenarioResult.Failure("Unknown error occurred."),
        };
    }
}
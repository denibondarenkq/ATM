using Application.Models.Balance;

namespace Application.Contracts.Users;

public interface ICustomerService
{
    LoginResult Login(string username, string password);
    CustomerResult FindCustomerByUsername(string username);
    UpdateBalanceResult WithdrawMoney(string username, decimal amount);
    UpdateBalanceResult DepositMoney(string username, decimal amount);
    CheckBalanceResult CheckBalance(string username);
}

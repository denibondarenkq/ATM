using Application.Abstractions.Repositories;
using Application.Contracts.Users;
using Application.Models.Balance;
using Application.Models.Users;

namespace Application.App.Users;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CurrentUserService _currentUserService;

    public CustomerService(ICustomerRepository customerRepository, CurrentUserService currentUserService)
    {
        _customerRepository = customerRepository;
        _currentUserService = currentUserService;
    }

    public LoginResult Login(string username, string password)
    {
        Customer? customer = _customerRepository.FindCustomerByUsername(username).Result;
        if (customer is null || customer.Password != password)
        {
            return new LoginResult.NotFound();
        }

        _currentUserService.CurrentCustomer = customer;
        return new LoginResult.Success();
    }

    public CustomerResult FindCustomerByUsername(string username)
    {
        Customer? customer = _customerRepository.FindCustomerByUsername(username).Result;
        if (customer is null)
        {
            return new CustomerResult.NotFound();
        }

        return new CustomerResult.Success(customer);
    }

    public UpdateBalanceResult WithdrawMoney(string username, decimal amount)
    {
        Customer? customer = _customerRepository.FindCustomerByUsername(username).Result;
        if (customer is null)
        {
            return new UpdateBalanceResult.Failure("Customer not found");
        }

        decimal newBalance = customer.Balance - amount;
        if (newBalance < 0)
        {
            return new UpdateBalanceResult.Failure("Insufficient funds");
        }

        return _customerRepository.UpdateBalance(username, newBalance).Result;
    }

    public UpdateBalanceResult DepositMoney(string username, decimal amount)
    {
        Customer? customer = _customerRepository.FindCustomerByUsername(username).Result;
        if (customer is null)
        {
            return new UpdateBalanceResult.Failure("Customer not found");
        }

        decimal newBalance = customer.Balance + amount;
        return _customerRepository.UpdateBalance(username, newBalance).Result;
    }

    public CheckBalanceResult CheckBalance(string username)
    {
        decimal balance = _customerRepository.GetBalance(username).Result;
        return new CheckBalanceResult.Success(balance);
    }
}

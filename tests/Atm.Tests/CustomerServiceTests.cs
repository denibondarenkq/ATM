using Application.Abstractions.Repositories;
using Application.App.Users;
using Application.Models.Balance;
using Application.Models.Users;
using Moq;
using Xunit;

namespace ObjectOrientedProgramming.Atm.Tests;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        var currentUserService = new CurrentUserService();
        _customerService = new CustomerService(_mockCustomerRepository.Object, currentUserService);
    }

    [Fact]
    public void WithdrawMoney_UpdatesBalanceCorrectly_WhenFundsAreSufficient()
    {
        string username = "user1";
        decimal startingBalance = 100m;
        decimal withdrawAmount = 50m;
        _mockCustomerRepository.Setup(repo => repo.FindCustomerByUsername(It.IsAny<string>()))
            .ReturnsAsync(new Customer(username, "password", startingBalance));
        _mockCustomerRepository.Setup(repo => repo.UpdateBalance(It.IsAny<string>(), It.IsAny<decimal>()))
            .ReturnsAsync(new UpdateBalanceResult.Success(startingBalance - withdrawAmount));

        UpdateBalanceResult result = _customerService.WithdrawMoney(username, withdrawAmount);

        Assert.IsType<UpdateBalanceResult.Success>(result);
        _mockCustomerRepository.Verify(repo => repo.UpdateBalance(username, startingBalance - withdrawAmount), Times.Once);
    }

    [Fact]
    public void WithdrawMoney_ReturnsFailure_WhenFundsAreInsufficient()
    {
        string username = "user1";
        decimal startingBalance = 30m;
        decimal withdrawAmount = 50m;
        _mockCustomerRepository.Setup(repo => repo.FindCustomerByUsername(It.IsAny<string>()))
            .ReturnsAsync(new Customer(username, "password", startingBalance));

        UpdateBalanceResult result = _customerService.WithdrawMoney(username, withdrawAmount);

        Assert.IsType<UpdateBalanceResult.Failure>(result);
    }

    [Fact]
    public void DepositMoney_UpdatesBalanceCorrectly_WhenAmountIsPositive()
    {
        string username = "user1";
        decimal startingBalance = 100m;
        decimal depositAmount = 50m;
        _mockCustomerRepository.Setup(repo => repo.FindCustomerByUsername(It.IsAny<string>()))
            .ReturnsAsync(new Customer(username, "password", startingBalance));
        _mockCustomerRepository.Setup(repo => repo.UpdateBalance(It.IsAny<string>(), It.IsAny<decimal>()))
            .ReturnsAsync(new UpdateBalanceResult.Success(startingBalance + depositAmount));

        UpdateBalanceResult result = _customerService.DepositMoney(username, depositAmount);

        Assert.IsType<UpdateBalanceResult.Success>(result);
        _mockCustomerRepository.Verify(repo => repo.UpdateBalance(username, startingBalance + depositAmount), Times.Once);
    }
}

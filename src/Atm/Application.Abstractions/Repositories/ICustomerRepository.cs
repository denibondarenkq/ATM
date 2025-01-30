using Application.Models.Balance;
using Application.Models.Users;

namespace Application.Abstractions.Repositories;

public interface ICustomerRepository
{
    public Task<CreateUserResult> CreateCustomer(string username, string password, decimal balance);
    public Task<Customer?> FindCustomerByUsername(string username);
    public Task<decimal> GetBalance(string username);
    public Task<UpdateBalanceResult> UpdateBalance(string username, decimal newBalance);
}
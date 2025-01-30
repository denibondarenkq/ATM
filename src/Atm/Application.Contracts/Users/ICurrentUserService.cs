using Application.Models.Users;

namespace Application.Contracts.Users;

public interface ICurrentUserService
{
    Admin? CurrentAdmin { get; }
    Customer? CurrentCustomer { get; }
}
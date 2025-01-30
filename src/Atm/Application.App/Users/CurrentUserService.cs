using Application.Contracts.Users;
using Application.Models.Users;

namespace Application.App.Users;

public class CurrentUserService : ICurrentUserService
{
    public Admin? CurrentAdmin { get; set; }
    public Customer? CurrentCustomer { get; set; }
}

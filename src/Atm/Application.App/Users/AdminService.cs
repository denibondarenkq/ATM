using Application.Abstractions.Repositories;
using Application.Contracts.Users;
using Application.Models.Users;

namespace Application.App.Users;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly CurrentUserService _currentUserService;

    public AdminService(IAdminRepository adminRepository, ICustomerRepository customerRepository, CurrentUserService currentUserService)
    {
        _adminRepository = adminRepository;
        _customerRepository = customerRepository;
        _currentUserService = currentUserService;
    }

    public LoginResult Login(string username, string password)
    {
        Admin? admin = _adminRepository.FindAdminByUsername(username).Result;
        if (admin is null || admin.Password != password)
        {
            return new LoginResult.NotFound();
        }

        _currentUserService.CurrentAdmin = admin;
        return new LoginResult.Success();
    }

    public AdminResult FindAdminByUsername(string username)
    {
        Admin? admin = _adminRepository.FindAdminByUsername(username).Result;
        if (admin is null)
        {
            return new AdminResult.NotFound();
        }

        return new AdminResult.Success(admin);
    }

    public ChangePasswordResult ChangeAdminPassword(string username, string newPassword)
    {
        CreateUserResult result = _adminRepository.ChangeAdminPassword(username, newPassword).Result;
        if (result is CreateUserResult.Success)
        {
            return new ChangePasswordResult.Success();
        }

        return new ChangePasswordResult.Failure("Failed to change password");
    }

    public CreateUserResult CreateCustomer(string username, string password, decimal initialBalance)
    {
        Customer? customer = _customerRepository.FindCustomerByUsername(username).Result;
        Admin? admin = _adminRepository.FindAdminByUsername(username).Result;
        if (customer != null || admin != null)
        {
            return new CreateUserResult.Failure("Username is already taken.");
        }

        CreateUserResult result = _customerRepository.CreateCustomer(username, password, initialBalance).Result;
        return result;
    }

    public CreateUserResult CreateAdmin(string username, string password)
    {
        Customer? customer = _customerRepository.FindCustomerByUsername(username).Result;
        Admin? admin = _adminRepository.FindAdminByUsername(username).Result;
        if (customer != null || admin != null)
        {
            return new CreateUserResult.Failure("Username is already taken.");
        }

        CreateUserResult result = _adminRepository.CreateAdmin(username, password).Result;
        return result;
    }
}
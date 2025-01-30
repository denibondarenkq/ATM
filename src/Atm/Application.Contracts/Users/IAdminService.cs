using Application.Models.Users;

namespace Application.Contracts.Users;

public interface IAdminService
{
    LoginResult Login(string username, string password);
    AdminResult FindAdminByUsername(string username);
    ChangePasswordResult ChangeAdminPassword(string username, string newPassword);
    CreateUserResult CreateCustomer(string username, string password, decimal initialBalance);
    CreateUserResult CreateAdmin(string username, string password);
}

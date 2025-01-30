using Application.Models.Users;

namespace Application.Abstractions.Repositories;

public interface IAdminRepository
{
    public Task<CreateUserResult> CreateAdmin(string username, string password);
    public Task<Admin?> FindAdminByUsername(string username);
    public Task<CreateUserResult> ChangeAdminPassword(string username, string newPassword);
}
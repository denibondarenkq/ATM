using Application.Abstractions.Repositories;
using Application.Models.Users;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<CreateUserResult> CreateAdmin(string username, string password)
    {
        const string sql = @"INSERT INTO Admins (Username, Password) VALUES (@Username, @Password);";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);
        int result = await command.ExecuteNonQueryAsync();
        return result > 0 ? new CreateUserResult.Success(username) : new CreateUserResult.Failure("Failed to create admin.");
    }

    public async Task<Admin?> FindAdminByUsername(string username)
    {
        const string sql = @"SELECT Username, Password FROM Admins WHERE Username = @Username;";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync() is false) return null;
        return new Admin(reader.GetString(0), reader.GetString(1));
    }

    public async Task<CreateUserResult> ChangeAdminPassword(string username, string newPassword)
    {
        const string sql = @"UPDATE Admins SET Password = @Password WHERE Username = @Username;";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", newPassword);
        int result = await command.ExecuteNonQueryAsync();
        return result > 0 ? new CreateUserResult.Success(username) : new CreateUserResult.Failure("Failed to update password.");
    }
}

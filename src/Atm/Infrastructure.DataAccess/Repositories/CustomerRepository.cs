using Application.Abstractions.Repositories;
using Application.Models.Balance;
using Application.Models.Users;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public CustomerRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<CreateUserResult> CreateCustomer(string username, string password, decimal balance)
    {
        const string sql = @"INSERT INTO Customers (Username, Password, Balance) VALUES (@Username, @Password, @Balance);";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);
        command.Parameters.AddWithValue("@Balance", balance);
        int result = await command.ExecuteNonQueryAsync();
        return result > 0 ? new CreateUserResult.Success(username) : new CreateUserResult.Failure("Failed to create customer.");
    }

    public async Task<Customer?> FindCustomerByUsername(string username)
    {
        const string sql = @"SELECT Username, Password, Balance FROM Customers WHERE Username = @Username;";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync() is false) return null;
        return new Customer(reader.GetString(0), reader.GetString(1), reader.GetDecimal(2));
    }

    public async Task<int?> GetCustomerIdByUsername(string username)
    {
        const string sql = @"SELECT CustomerID FROM Customers WHERE Username = @Username;";
        await using NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        object? result = await command.ExecuteScalarAsync();
        if (result == DBNull.Value || result == null)
        {
            return null;
        }

        return Convert.ToInt32(result, System.Globalization.CultureInfo.InvariantCulture);
    }

    public async Task<decimal> GetBalance(string username)
    {
        const string sql = @"SELECT Balance FROM Customers WHERE Username = @Username;";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync() is false) return 0;
        return reader.GetDecimal(0);
    }

    public async Task<UpdateBalanceResult> UpdateBalance(string username, decimal newBalance)
    {
        const string sql = @"UPDATE Customers SET Balance = @Balance WHERE Username = @Username;";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Balance", newBalance);
        int result = await command.ExecuteNonQueryAsync();
        return result > 0 ? new UpdateBalanceResult.Success(newBalance) : new UpdateBalanceResult.Failure("Failed to update balance.");
    }
}

using Application.Abstractions.Repositories;
using Application.Models.Transactions;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task RecordTransaction(string username, TransactionType type, decimal amount)
    {
        string typeAsString = type.ToString();
        const string sql = @"INSERT INTO Transactions (Username, Type, Amount) VALUES (@Username, @Type, @Amount);";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Type", typeAsString); // Используем строковое представление типа
        command.Parameters.AddWithValue("@Amount", amount);
        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Transaction>> ViewTransactionHistoryByCustomer(string username)
    {
        const string sql = @"SELECT Username, Type, Amount FROM Transactions WHERE Username = @Username;";
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);

        var transactions = new List<Transaction>();
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            transactions.Add(new Transaction(
                reader.GetString(reader.GetOrdinal("Username")),
                Enum.Parse<TransactionType>(reader.GetString(reader.GetOrdinal("Type"))),
                reader.GetDecimal(reader.GetOrdinal("Amount"))));
        }

        return transactions;
    }
}
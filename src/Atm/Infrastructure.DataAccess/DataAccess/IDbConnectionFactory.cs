using Npgsql;

namespace Infrastructure.DataAccess.DataAccess;

public interface IDbConnectionFactory
{
    NpgsqlConnection CreateConnection();
}
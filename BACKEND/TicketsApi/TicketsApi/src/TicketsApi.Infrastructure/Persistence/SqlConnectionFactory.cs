using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace TicketsApi.Infrastructure.Persistence;

public interface IDbConnectionFactory
{
    /// <summary>Devuelve una conexión ya abierta. Quien la pide la cierra con <c>await using</c>.</summary>
    Task<DbConnection> OpenAsync(CancellationToken ct);
}

public sealed class SqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public async Task<DbConnection> OpenAsync(CancellationToken ct)
    {
        var connection = new SqlConnection(connectionString);
        try
        {
            await connection.OpenAsync(ct);
            return connection;
        }
        catch
        {
            await connection.DisposeAsync();
            throw;
        }
    }
}

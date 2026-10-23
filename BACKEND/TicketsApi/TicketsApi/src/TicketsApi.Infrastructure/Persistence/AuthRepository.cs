using Dapper;
using TicketsApi.Application.Auth;

namespace TicketsApi.Infrastructure.Persistence;

public class AuthRepository(IDbConnectionFactory connectionFactory) : IAuthRepository
{
    public async Task<AuthUser?> GetByUserNameAsync(string userName, CancellationToken ct)
    {
        await using var connection = await connectionFactory.OpenAsync(ct);
        return await connection.QuerySingleOrDefaultAsync<AuthUser>(
            new CommandDefinition(AuthSql.GetByUserName, new { UserName = userName }, cancellationToken: ct));
    }

    public async Task<IReadOnlyList<MenuItemDto>> GetUserMenuAsync(int userId, CancellationToken ct)
    {
        await using var connection = await connectionFactory.OpenAsync(ct);
        var rows = await connection.QueryAsync<MenuItemDto>(
            new CommandDefinition(AuthSql.GetUserMenu, new { UserId = userId }, cancellationToken: ct));

        return rows.ToList();
    }

    public async Task UpdateLastLoginAsync(int userId, CancellationToken ct)
    {
        await using var connection = await connectionFactory.OpenAsync(ct);
        await connection.ExecuteAsync(
            new CommandDefinition(AuthSql.UpdateLastLogin, new { UserId = userId }, cancellationToken: ct));
    }
}
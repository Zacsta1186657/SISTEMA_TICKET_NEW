namespace TicketsApi.Infrastructure.Persistence;

internal static class AuthSql
{
    // Si la persona está inactiva, no devuelve fila y el login falla igual que una clave mala.
    public const string GetByUserName = """
        SELECT u.UserId, u.UserName, u.PasswordHash, u.IsActive, u.MustChangePassword
        FROM seg.[User] u
        JOIN org.Person p ON p.PersonId = u.PersonId
        WHERE u.UserName = @UserName
          AND p.IsActive = 1;
        """;

    public const string UpdateLastLogin = """
        UPDATE seg.[User] SET LastLoginAt = SYSDATETIME() WHERE UserId = @UserId;
        """;

    public const string GetUserMenu = """
        SELECT MenuId, ParentMenuId, Code, Name, Url, Icon, SortOrder
        FROM seg.vw_UserMenu
        WHERE UserId = @UserId
        ORDER BY SortOrder;
        """;
}
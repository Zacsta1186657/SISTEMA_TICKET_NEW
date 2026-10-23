namespace TicketsApi.Application.Auth;

public interface IAuthRepository
{
    Task<AuthUser?> GetByUserNameAsync(string userName, CancellationToken ct);
    Task UpdateLastLoginAsync(int userId, CancellationToken ct);
    Task<IReadOnlyList<MenuItemDto>> GetUserMenuAsync(int userId, CancellationToken ct);
}
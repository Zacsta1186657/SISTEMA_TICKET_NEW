using TicketsApi.Application.Auth;
using TicketsApi.Application.Users;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct);
    Task<List<MenuNode>> GetMenuAsync(int userId, CancellationToken ct);
}


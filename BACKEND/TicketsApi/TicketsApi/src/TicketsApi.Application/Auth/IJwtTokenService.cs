namespace TicketsApi.Application.Auth;

public interface IJwtTokenService
{
    (string Token, DateTime ExpiresAt) Generate(AuthUser user);
}
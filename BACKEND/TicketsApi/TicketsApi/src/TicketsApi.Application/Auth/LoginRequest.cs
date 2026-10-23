namespace TicketsApi.Application.Auth;

public record LoginRequest(string UserName, string Password);

public record LoginResponse(string Token, DateTime ExpiresAt, bool MustChangePassword);
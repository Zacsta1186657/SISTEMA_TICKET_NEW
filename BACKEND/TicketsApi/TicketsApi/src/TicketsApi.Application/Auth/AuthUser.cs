namespace TicketsApi.Application.Auth;

// Lo mínimo que necesita el login. Los nombres coinciden con las columnas que consulta el repositorio.
public record AuthUser(int UserId, string UserName, string PasswordHash, bool IsActive, bool MustChangePassword);
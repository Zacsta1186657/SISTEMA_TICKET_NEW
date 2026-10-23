using FluentValidation;
using TicketsApi.Application.Exceptions;
using TicketsApi.Application.Security;

namespace TicketsApi.Application.Auth;

//public interface IAuthService
//{
//    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct);
//}

public class AuthService(
    IAuthRepository repository,
    IPasswordService passwords,
    IJwtTokenService jwt,
    IValidator<LoginRequest> validator) : IAuthService
{
    public async Task<List<MenuNode>> GetMenuAsync(int userId, CancellationToken ct) 
        => MenuTreeBuilder.Build(await repository.GetUserMenuAsync(userId, ct));

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);

        var user = await repository.GetByUserNameAsync(request.UserName.Trim(), ct);

        // Mismo mensaje para "no existe", "inactivo" y "clave incorrecta":
        // así nadie puede averiguar qué usuarios existen.
        if (user is null || !user.IsActive || !passwords.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Usuario o contraseña incorrectos.");

        await repository.UpdateLastLoginAsync(user.UserId, ct);

        var (token, expiresAt) = jwt.Generate(user);
        return new LoginResponse(token, expiresAt, user.MustChangePassword);
    }
}
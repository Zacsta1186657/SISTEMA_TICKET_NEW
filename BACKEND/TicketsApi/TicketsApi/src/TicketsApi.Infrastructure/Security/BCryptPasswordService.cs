using TicketsApi.Application.Security;

namespace TicketsApi.Infrastructure.Security;
public sealed class BCryptPasswordService : IPasswordService
{
    private const int WorkFactor = 12;   // costo del hash: más alto = más lento y más seguro

    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);

    public bool Verify(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(password, hash);
}
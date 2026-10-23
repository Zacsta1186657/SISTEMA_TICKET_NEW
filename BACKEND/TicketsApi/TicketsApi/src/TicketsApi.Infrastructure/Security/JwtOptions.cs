namespace TicketsApi.Infrastructure.Security;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Key { get; set; } = "";          // secreto para firmar (mínimo 32 caracteres)
    public string Issuer { get; set; } = "";       // quién emite el token
    public string Audience { get; set; } = "";     // para quién es
    public int ExpirationMinutes { get; set; } = 60;
}
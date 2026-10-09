namespace TicketsApi.Application.Security;
public interface IPasswordService
{
    string Hash(string password);
    bool Verify(string password, string hash);   // la usarás en el login
}
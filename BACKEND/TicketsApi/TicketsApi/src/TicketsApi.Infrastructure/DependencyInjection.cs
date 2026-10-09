using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketsApi.Application.Persons;
using TicketsApi.Application.Security;
using TicketsApi.Infrastructure.Persistence;
using TicketsApi.Infrastructure.Security;

namespace TicketsApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer")
            ?? throw new InvalidOperationException("Falta la cadena de conexión ConnectionStrings:SqlServer.");

        services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connectionString));
        services.AddSingleton<IPasswordService, BCryptPasswordService>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}

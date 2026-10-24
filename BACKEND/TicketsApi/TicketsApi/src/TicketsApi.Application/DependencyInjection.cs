using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketsApi.Application.Auth;
using TicketsApi.Application.Persons;
using TicketsApi.Application.Profiles;
using TicketsApi.Application.Users;

namespace TicketsApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProfileService, ProfileService>();
        return services;
    }
}

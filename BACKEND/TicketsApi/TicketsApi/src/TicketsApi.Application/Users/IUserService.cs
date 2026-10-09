using TicketsApi.Application.Users;

public interface IUserService 
{
    Task<CreateUserResponse> CreateAsync(CreateUserRequest request, CancellationToken ct);
}

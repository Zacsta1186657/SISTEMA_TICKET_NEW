using TicketsApi.Domain.Entities;

public interface IUserRepository
{
    Task<int> CreateAsync(User user, int profileId, int createdBy, CancellationToken ct);
}


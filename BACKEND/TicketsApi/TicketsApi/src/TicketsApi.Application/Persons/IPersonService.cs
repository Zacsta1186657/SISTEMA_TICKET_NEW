namespace TicketsApi.Application.Persons;

public interface IPersonService
{
    Task<CreatePersonResponse> CreateAsync(CreatePersonRequest request, CancellationToken ct);
}

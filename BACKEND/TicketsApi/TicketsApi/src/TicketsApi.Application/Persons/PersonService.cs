using FluentValidation;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Application.Persons;

public class PersonService(
    IPersonRepository repository,
    IValidator<CreatePersonRequest> validator) : IPersonService
{
    public async Task<CreatePersonResponse> CreateAsync(CreatePersonRequest request, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(request, ct);

        // Correo y teléfono vacíos se guardan como NULL: el índice único de correo
        // solo admite varios NULL, no varios textos vacíos.
        var person = new Person
        {
            DocumentNumber = request.DocumentNumber.Trim(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = NullIfBlank(request.Email)?.ToLowerInvariant(),
            Phone = NullIfBlank(request.Phone)
        };

        var personId = await repository.CreateAsync(person, request.CreatedBy, ct);
        return new CreatePersonResponse(personId);
    }

    private static string? NullIfBlank(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    public Task<IReadOnlyList<PersonDto>> ListAsync(CancellationToken ct)
        => repository.ListAsync(ct);
}

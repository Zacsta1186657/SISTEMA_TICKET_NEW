using TicketsApi.Domain.Entities;

namespace TicketsApi.Application.Persons;

public interface IPersonRepository
{
    /// <summary>Inserta la persona y devuelve su PersonId.</summary>
    /// <exception cref="Exceptions.ConflictException">Documento o correo ya registrados.</exception>
    /// <exception cref="Exceptions.ReferenceNotFoundException">createdBy no existe.</exception>
    Task<int> CreateAsync(Person person, int createdBy, CancellationToken ct);
}

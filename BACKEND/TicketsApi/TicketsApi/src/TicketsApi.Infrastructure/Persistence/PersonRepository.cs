using Dapper;
using Microsoft.Data.SqlClient;
using TicketsApi.Application.Exceptions;
using TicketsApi.Application.Persons;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Infrastructure.Persistence;

public class PersonRepository(IDbConnectionFactory connectionFactory) : IPersonRepository
{
    public async Task<int> CreateAsync(Person person, int createdBy, CancellationToken ct)
    {
        await using var connection = await connectionFactory.OpenAsync(ct);

        try
        {
            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                PersonSql.Insert,
                new
                {
                    person.DocumentNumber,
                    person.FirstName,
                    person.LastName,
                    person.Email,
                    person.Phone,
                    CreatedBy = createdBy
                },
                cancellationToken: ct));
        }
        // 2627 = restricción UNIQUE, 2601 = índice único (el de correo es un índice filtrado)
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            if (ex.Message.Contains("UQ_Person_DocumentNumber"))
                throw new ConflictException("Ya existe una persona con ese número de documento.");
            if (ex.Message.Contains("UX_Person_Email"))
                throw new ConflictException("Ya existe una persona con ese correo.");
            throw;
        }
        // 547 = clave foránea: CreatedBy no es un UserId existente
        catch (SqlException ex) when (ex.Number == 547)
        {
            throw new ReferenceNotFoundException("El usuario indicado en CreatedBy no existe.");
        }
    }

    public async Task<IReadOnlyList<PersonDto>> ListAsync(CancellationToken ct)
    {
        await using var connection = await connectionFactory.OpenAsync(ct);
        var rows = await connection.QueryAsync<PersonDto>(
            new CommandDefinition(PersonSql.List, cancellationToken: ct));
        return rows.ToList();
    }
}

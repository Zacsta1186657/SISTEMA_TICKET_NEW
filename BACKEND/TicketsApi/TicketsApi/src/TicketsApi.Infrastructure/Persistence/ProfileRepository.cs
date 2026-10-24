using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TicketsApi.Application.Exceptions;
using TicketsApi.Application.Profiles;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Infrastructure.Persistence
{
    internal class ProfileRepository(IDbConnectionFactory connectionFactory) : IProfileRepository
    {
        public async Task<int> CreateAsync(Profile profile, int createdBy, CancellationToken ct)
        {
            await using var connection = await connectionFactory.OpenAsync(ct);

            try
            {
                return await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                    ProfileSql.Insert,
                    new { profile.Name, profile.Description, CreatedBy = createdBy },
                    cancellationToken: ct));


            }
            // 2627 = restricción UNIQUE, 2601 = índice único
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                if (ex.Message.Contains("UQ_Profile_Name"))
                    throw new ConflictException("Ya existe un perfil con ese nombre.");
                throw;
            }
            // 547 = clave foránea: CreatedBy no es un UserId existente
            catch(SqlException ex) when (ex.Number == 547)
            {
                throw new ReferenceNotFoundException("El usuario que registra el perfil no existe.");
            }
        }

        public async Task<IReadOnlyList<ProfileDto>> ListAsync(CancellationToken ct)
        {
            await using var connection = await connectionFactory.OpenAsync(ct);

            var rows = await connection.QueryAsync<ProfileDto>(
                new CommandDefinition(ProfileSql.List, cancellationToken: ct));

            return rows.ToList();
        }
    }
}

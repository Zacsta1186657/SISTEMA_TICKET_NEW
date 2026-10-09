using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TicketsApi.Application.Exceptions;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Infrastructure.Persistence
{
    public class UserRepository(IDbConnectionFactory connectionFactory) : IUserRepository
    {
        public async Task<int> CreateAsync(User user, int profileId, int createdBy, CancellationToken ct)
        {
            await using var connection = await connectionFactory.OpenAsync(ct);

            try
            {
                return await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                    "seg.usp_User_Create",
                    new
                    {
                        user.PersonId,
                        ProfileId = profileId,
                        user.UserName,
                        user.PasswordHash,
                        CreatedBy = createdBy
                    },
                    commandType: CommandType.StoredProcedure,
                    cancellationToken: ct));
            }
            catch (SqlException)
            {
                throw new ReferenceNotFoundException("La persona, un perfil o el usuario creador no existe.");
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("UQ_User_UserName"))
                    throw new ConflictException("Ese nombre de usuario ya está en uso.");
                if (ex.Message.Contains("UQ_User_PersonId"))
                    throw new ConflictException("Esa persona ya tiene un usuario.");
                throw;
            }
        }
    }
}

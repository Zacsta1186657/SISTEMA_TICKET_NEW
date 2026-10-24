using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Infrastructure.Persistence;

internal static class ProfileSql
{
        // IsActive y CreatedAt los llena la base con sus valores por defecto.
   public const string Insert = """
    INSERT INTO seg.Profile (Name, Description, CreatedBy)
    OUTPUT INSERTED.ProfileId
    VALUES (@Name, @Description, @CreatedBy);
    """;

   public const string List = """
    SELECT ProfileId, Name, Description, IsActive, CreatedAt
    FROM seg.Profile
    ORDER BY Name;
    """;

}

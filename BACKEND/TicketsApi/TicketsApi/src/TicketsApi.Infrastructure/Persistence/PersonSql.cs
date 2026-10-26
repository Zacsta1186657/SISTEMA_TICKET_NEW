using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Infrastructure.Persistence
{
    internal class PersonSql
    {
        public const string Insert = """
        INSERT INTO org.Person (DocumentNumber, FirstName, LastName, Email, Phone, CreatedBy)
        OUTPUT INSERTED.PersonId
        VALUES (@DocumentNumber, @FirstName, @LastName, @Email, @Phone, @CreatedBy);
        """;

        public const string List = """
        SELECT PersonId, DocumentNumber, FirstName, LastName, Email, Phone, IsActive, CreatedAt
        FROM org.Person WHERE FirstName != 'Administrador' OR PersonId != 2
        ORDER BY CreatedAt DESC;
        """;
    }
}

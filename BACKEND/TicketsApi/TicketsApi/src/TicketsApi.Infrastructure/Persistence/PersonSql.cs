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
    }
}

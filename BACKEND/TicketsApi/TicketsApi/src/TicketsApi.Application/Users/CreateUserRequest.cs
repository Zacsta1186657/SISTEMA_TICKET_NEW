using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Users
{
    public record CreateUserRequest
    (
        int PersonId,
        int ProfileId,
        string UserName,
        string Password,
        int CreatedBy
    );

    public record CreateUserResponse(int UserId);
}

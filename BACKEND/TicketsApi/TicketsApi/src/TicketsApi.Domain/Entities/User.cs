using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required int PersonId { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public bool MustChangePassword { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }
}

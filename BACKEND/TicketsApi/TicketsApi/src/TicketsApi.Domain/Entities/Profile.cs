using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Domain.Entities
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}

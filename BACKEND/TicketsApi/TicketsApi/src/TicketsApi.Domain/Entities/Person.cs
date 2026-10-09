namespace TicketsApi.Domain.Entities;

/// <summary>Persona real (no la cuenta de acceso). Tabla org.Person.</summary>
public class Person
{
    public int PersonId { get; set; }
    public required string DocumentNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

namespace TicketsApi.Application.Persons;

/// <param name="CreatedBy">UserId de quien registra. Mientras no haya login se envía a mano.</param>
public record CreatePersonRequest(
    string DocumentNumber,
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    int CreatedBy);

public record CreatePersonResponse(int PersonId);

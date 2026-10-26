using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Persons;

public record CreatePersonRequest(
    string DocumentNumber,
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    int CreatedBy);

public record CreatePersonResponse(int PersonId);

public record PersonDto(
    int PersonId,
    string DocumentNumber,
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    bool IsActive,
    DateTime CreatedAt);
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Profiles;

// CreatedBy no viaja aquí: sale del token del usuario que hace la petición.
public record CreateProfileRequest(string Name, string? Description);

public record CreateProfileResponse(int ProfileId);

/// <summary>Perfil tal como se devuelve en el listado.</summary>
public record ProfileDto(int  ProfileId, string Name, string? Description, bool IsActive, DateTime CreatedAt);


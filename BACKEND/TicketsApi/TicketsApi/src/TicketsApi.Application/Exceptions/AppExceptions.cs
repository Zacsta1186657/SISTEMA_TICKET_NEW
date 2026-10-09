namespace TicketsApi.Application.Exceptions;

/// <summary>Regla de negocio que choca con datos existentes (la API responde 409).</summary>
public class ConflictException(string message) : Exception(message);

/// <summary>Un Id enviado no existe, por ejemplo CreatedBy (la API responde 400).</summary>
public class ReferenceNotFoundException(string message) : Exception(message);

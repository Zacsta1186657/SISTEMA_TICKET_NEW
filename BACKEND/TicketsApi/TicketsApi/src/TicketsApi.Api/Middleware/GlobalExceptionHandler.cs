using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketsApi.Application.Exceptions;

namespace TicketsApi.Api.Middleware;

/// <summary>Convierte las excepciones de la aplicación en respuestas HTTP (ProblemDetails).</summary>
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        object body;
        int status;

        switch (exception)
        {
            case ValidationException validation:
                status = StatusCodes.Status400BadRequest;
                body = new ValidationProblemDetails(validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray()))
                {
                    Status = status,
                    Title = "Hay datos inválidos."
                };
                break;

            case ConflictException conflict:
                status = StatusCodes.Status409Conflict;
                body = new ProblemDetails { Status = status, Title = "Registro duplicado.", Detail = conflict.Message };
                break;

            case ReferenceNotFoundException reference:
                status = StatusCodes.Status400BadRequest;
                body = new ProblemDetails { Status = status, Title = "Referencia inválida.", Detail = reference.Message };
                break;

            case UnauthorizedException unauthorized:
                status = StatusCodes.Status401Unauthorized;
                body = new ProblemDetails { Status = status, Title = "No autorizado.", Detail = unauthorized.Message };
                break;

            default:
                logger.LogError(exception, "Error no controlado en {Method} {Path}",
                    context.Request.Method, context.Request.Path);
                status = StatusCodes.Status500InternalServerError;
                body = new ProblemDetails { Status = status, Title = "Ocurrió un error inesperado." };
                break;
        }

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(body, body.GetType(), cancellationToken: ct);
        return true;
    }
}

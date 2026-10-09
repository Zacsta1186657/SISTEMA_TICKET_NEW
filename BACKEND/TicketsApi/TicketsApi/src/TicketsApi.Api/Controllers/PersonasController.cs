using Microsoft.AspNetCore.Mvc;
using TicketsApi.Application.Persons;

namespace TicketsApi.Api.Controllers;

[ApiController]
[Route("api/personas")]
public class PersonasController(IPersonService personService) : ControllerBase
{
    /// <summary>Crea una persona en org.Person.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(CreatePersonRequest request, CancellationToken ct)
    {
        var response = await personService.CreateAsync(request, ct);
        return StatusCode(StatusCodes.Status201Created, response);
    }
}

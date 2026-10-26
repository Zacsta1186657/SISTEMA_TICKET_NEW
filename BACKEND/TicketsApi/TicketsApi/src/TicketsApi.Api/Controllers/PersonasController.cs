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

    /// <summary>Lista todas las personas.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PersonDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var persons = await personService.ListAsync(ct);
        return Ok(persons);
    }
}

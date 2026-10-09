using Microsoft.AspNetCore.Mvc;
using TicketsApi.Application.Users;

namespace TicketsApi.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateUserRequest request, CancellationToken ct)
        {
            var response = await userService.CreateAsync(request, ct);
            return StatusCode(StatusCodes.Status201Created, response);
        }

    }
}

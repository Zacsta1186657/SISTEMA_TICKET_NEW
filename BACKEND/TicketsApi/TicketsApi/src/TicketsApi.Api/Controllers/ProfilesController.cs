using Microsoft.AspNetCore.Mvc;
using TicketsApi.Application.Profiles;

namespace TicketsApi.Api.Controllers
{
    [ApiController]
    [Route("api/perfiles")]
    public class ProfilesController(IProfileService profileService) : ControllerBase
    {
        /// <summary>Crea un perfil en seg.Profile. Queda activo y con la fecha actual.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateProfileResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateProfileRequest request, CancellationToken ct)
        {
            var userId = int.Parse(User.FindFirst("sub")!.Value);
            var response = await profileService.CreateAsync(request, userId, ct);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        /// <summary>Lista todos los perfiles.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ProfileDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> List(CancellationToken ct)
            => Ok(await profileService.ListAsync(ct));

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsApi.Application.Auth;

namespace TicketsApi.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutenticacionController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Auth(LoginRequest request, CancellationToken ct)
        {
           return Ok(await authService.LoginAsync(request, ct));
        }

        [HttpGet("menu")]
        [ProducesResponseType(typeof(List<MenuNode>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenu(CancellationToken ct)
        {
            var userId = int.Parse(User.FindFirst("sub")!.Value);
            return Ok(await authService.GetMenuAsync(userId, ct));
        }
    }
}

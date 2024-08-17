using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnassignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator): ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("userRoles")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRoles")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> DeleteUserRole([FromBody] UnassignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.AssignRole;
using Restaurant.Application.Users.Command;
using Restaurant.Application.Users.UnAssignRole;
using Restaurant.Model.Constant;

namespace Restaurents_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator mediator;

        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPatch("UpdateUserData")]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpPost("AssignRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("DeleteRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnAssignRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}

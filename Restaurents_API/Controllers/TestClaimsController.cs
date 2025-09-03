using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurents_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestClaimsController : ControllerBase
    {
        [HttpGet("me")]
        public IActionResult GetMyClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }
    }
}

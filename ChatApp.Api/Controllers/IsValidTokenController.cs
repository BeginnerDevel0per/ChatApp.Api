using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IsValidTokenController : ControllerBase
    {
        [HttpGet]
        public IActionResult IsToken()
        {
            return Ok();
        }
    }
}

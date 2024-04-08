using ChatApp.Core.Services;
using ChatApp.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _userService;
        public LoginController(ILoginService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            return Ok(await _userService.Login(UserName, Password));
        }

    }
}

using AutoMapper;
using ChatApp.Core.Services;
using ChatApp.Entities.Entities;
using ChatApp.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper mapper;
        public RegisterController(IMapper mapper, IUserService userService)
        {

            this.mapper = mapper;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            await _userService.Register(userRegister);
            return Ok("Kayıt Başarılı");
        }
    }
}

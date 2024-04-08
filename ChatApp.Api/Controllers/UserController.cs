using ChatApp.Core.Services;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/SearchUser/{UserName}")]
        public async Task<IActionResult> SearchUserAsync(string UserName)
        {
            return Ok(await _userService.SearchUsers(UserName));
        }

        [HttpGet]
        [Route("/GetProfile")]
        public async Task<IActionResult> GetProfileAsync()
        {
            return Ok(await _userService.LoggedInUserProfile());
        }

        [HttpPut]
        [Route("/UpdateProfile")]
        public async Task<IActionResult> UpdateProfileAsync(UpdateProfileDto updateProfileDto)
        {
            await _userService.UpdateProfile(updateProfileDto);
            return Ok();
        }

        [HttpPut]
        [Route("/UpdatePassword")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            await _userService.UpdatePassword(updatePasswordDto);
            return Ok();
        }


        [HttpGet]
        [Route("/GetFriendRequests")]
        public IActionResult GetFriendRequests()
        {
            return Ok(_userService.GetFriendRequestList());
        }

        [HttpGet]
        [Route("/GetFriends")]
        public IActionResult GetFriends()
        {
            return Ok(_userService.GetFriendList());
        }

        [HttpGet]
        [Route("/GetUserProfile/{GetUserId}")]
        public async Task<IActionResult> GetUserProfile(Guid GetUserId)
        {
            return Ok(await _userService.GetUserProfile(GetUserId));
        }

        [HttpGet]
        [Route("/AcceptFriendRequest/{AcceptId}")]
        public async Task<IActionResult> AcceptFriendRequest(Guid AcceptId)
        {
            await _userService.AcceptFriendRequest(AcceptId);
            return Ok();
        }

        [HttpGet]
        [Route("/SendFriendRequest/{SendId}")]
        public async Task<IActionResult> SendFriendRequest(Guid SendId)
        {
            await _userService.SendFriendRequest(SendId);
            return Ok();
        }


        [HttpDelete]
        [Route("/RemoveFriendRequestOrFriend/{RemoveId}")]
        public async Task<IActionResult> RemoveFriendRequestOrFriend(Guid RemoveId)
        {
            await _userService.RemoveFromYourFriendOrRequestList(RemoveId);
            return Ok();
        }



        [HttpPost]
        [Route("/UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile Image)
        {
            await _userService.UploadProfileImage(Image);
            return Ok();
        }

        [HttpDelete]
        [Route("/RemoveProfileImage")]
        public async Task<IActionResult> RemoveProfileImage()
        {
            await _userService.RemoveProfileImage();
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("/UserProfileImage/{ImagePathName}")]
        public IActionResult GetUserProfileImage(string ImagePathName)
        {
            return Ok(_userService.GetProfileImage(ImagePathName));
        }
    }
}

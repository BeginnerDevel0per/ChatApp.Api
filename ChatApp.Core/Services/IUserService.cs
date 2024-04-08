using ChatApp.Entities.Entities;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Services
{
    public interface IUserService : IGenericService<User>
    {
        Stream? GetProfileImage(string PathName);
        Task<List<SearchUserDto>> SearchUsers(string UserName);

        Task Register(UserRegister userRegister);

        Task UpdatePassword(UpdatePasswordDto updatePasswordDto);
        Task UpdateProfile(UpdateProfileDto updateProfileDto);
        Task UploadProfileImage(IFormFile Image);

        Task RemoveProfileImage();

        Task<ProfileDto> LoggedInUserProfile();



        List<UserDto> GetFriendRequestList();


        Task<GetUserProfileDto?> GetUserProfile(Guid UserIdToFetch);
        List<UserDto> GetFriendList();

        Task SendFriendRequest(Guid Id);

        Task AcceptFriendRequest(Guid AcceptId);

        Task RemoveFromYourFriendOrRequestList(Guid RemoveId);
    }
}

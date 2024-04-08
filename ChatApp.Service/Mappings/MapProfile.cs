using AutoMapper;
using ChatApp.Entities.Entities;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;


namespace ChatApp.Service.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UserRegister, User>();
            CreateMap<User, SearchUserDto>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserIdAndNameDto>();
            CreateMap<User, ProfileDto>();
            CreateMap<UpdateProfileDto, User>();
            CreateMap<Message, MessageDto>();
            CreateMap<MessageAndUser, LastMessageDto>();
            CreateMap<FriendsOfUser, FriendOfUsersDto>().ReverseMap();
        }
    }
}

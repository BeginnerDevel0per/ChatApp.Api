using AutoMapper;
using ChatApp.Core.StorageService;
using ChatApp.Core.Repositories;
using ChatApp.Core.Security;
using ChatApp.Core.Services;
using ChatApp.Core.UnifOfWorks;
using ChatApp.Entities.Entities;
using ChatApp.Service.Exceptions;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace ChatApp.Service.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGetInformationFromToken _getInformationFromToken;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileImage _userProfileImage;
        private readonly IMapper _mapper;
        private readonly IGeneratePasswordHash _generatePasswordHash;
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IGetInformationFromToken getInformationFromToken, IUserProfileImage userProfileImage, IGeneratePasswordHash generatePasswordHash) : base(repository, unitOfWork)
        {
            _getInformationFromToken = getInformationFromToken;
            _userRepository = userRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userProfileImage = userProfileImage;
            _generatePasswordHash = generatePasswordHash;
        }

        public Stream? GetProfileImage(string PathName)
        {
            return _userProfileImage.GetImage(PathName);
        }

        public async Task<List<SearchUserDto>> SearchUsers(string UserName)
        {
            var Users = await _userRepository.SearchUsers(UserName).ToListAsync();
            if (Users.Count > 0)
            {
                var SearcherUserInformation = _getInformationFromToken.GetUserIdAndUserName();
                var SearcherUser = Users.FirstOrDefault(x => x.Id == SearcherUserInformation.Id);
                if (SearcherUser != null)
                {
                    Users.Remove(SearcherUser);
                }

                var UsersMapping = _mapper.Map<List<SearchUserDto>>(Users);
                return UsersMapping;
            }
            throw new ClientSideException("Bu Kullanıcı adına sahip kullanıcı bulunamadı.");
        }

        public async Task Register(UserRegister userRegister)
        {
            if (await _userRepository.AnyAsync(x => x.UserName == userRegister.UserName.Trim()))
                throw new ClientSideException("Bu kullanıcı adı kullanılıyor");

            var User = _mapper.Map<User>(userRegister);
            User.Password = _generatePasswordHash.GeneratePasswordHash(User.Password);
            await _userRepository.AddAsync(User);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ProfileDto> LoggedInUserProfile()
        {
            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;

            var User = await _userRepository.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            if (User != null)
                return (_mapper.Map<ProfileDto>(User));
            else
                throw new UnauthorizedException("Kullanıcı Bulunamadı.");
        }

        public async Task UpdateProfile(UpdateProfileDto updateProfileDto)
        {
            updateProfileDto.Id = _getInformationFromToken.GetUserIdAndUserName().Id;


            if (await _userRepository.AnyAsync(x => x.Id != updateProfileDto.Id && x.UserName == updateProfileDto.UserName.Trim()))
                throw new ClientSideException("Bu kullanıcı adı kullanılıyor");

            var User = await _userRepository.Where(x => x.Id == updateProfileDto.Id).FirstOrDefaultAsync();
            if (User == null)
                throw new UnauthorizedException("Kullanıcı Bulunamadı.");

            var Usermapping = _mapper.Map(updateProfileDto, User);
            _userRepository.Update(Usermapping);
            await _unitOfWork.CommitAsync();
        }
        public async Task UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {

            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;
            var User = await _userRepository.Where(x => x.Id == UserId).FirstOrDefaultAsync();

            if (User == null)
                throw new UnauthorizedException("Kullanıcı Bulunamadı.");
            if (User.Password != updatePasswordDto.CurrentPassword)
                throw new ClientSideException("Şimdiki Şifre Hatalı.");



            User.Password = _generatePasswordHash.GeneratePasswordHash(updatePasswordDto.Password);
            User.PasswordUpdateDate = DateTime.Now;
            await _unitOfWork.CommitAsync();

        }


        public async Task UploadProfileImage(IFormFile Image)
        {


            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;
            var User = await _userRepository.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            if (User == null)
                throw new UnauthorizedException("Kullanıcı Bulunamadı.");



            if (!string.IsNullOrEmpty(User.ProfileImage))
            {//zaten profil resmi varsa UpdateImageAsync() methodunda onu siliyorum ve yeniyi ekliyorum.
                User.ProfileImage = await _userProfileImage.UpdateImageAsync(Image, User.ProfileImage);
            }
            else //profil resmi eğer yoksa sadece ekliyorum. UploadImageAsync() sadece resim dosyası yüklüyor.
                User.ProfileImage = await _userProfileImage.UploadImageAsync(Image);


            _userRepository.Update(User);
            await _unitOfWork.CommitAsync();

        }

        public async Task RemoveProfileImage()
        {
            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;

            var User = await _userRepository.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            if (User == null)
                throw new UnauthorizedException("Kullanıcı Bulunamadı.");

            if (!string.IsNullOrEmpty(User.ProfileImage))
            {
                var IsRemove = _userProfileImage.RemoveImageAsync(User.ProfileImage);
                User.ProfileImage = string.Empty;
                _userRepository.Update(User);
                await _unitOfWork.CommitAsync();
            }
            else
                throw new ClientSideException("Profil fotoğrafı zaten yok.");
        }


        public async Task<GetUserProfileDto?> GetUserProfile(Guid Id)
        {//burada arkadaş eklenmişmi ,istek varmı veya istek göndermişmi kontrol ediyorum.

            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;

            var FriendRequest = await _userRepository.IsFriendsOfUsersAsync(Id, UserId);
            var User = await _userRepository.GetUser(Id);
            GetUserProfileDto getUserProfileDto = new()
            {
                FriendOfUsersDto = _mapper.Map<FriendOfUsersDto>(FriendRequest),
                UserDto = _mapper.Map<UserDto>(User),

            };
            return getUserProfileDto;
        }


        public List<UserDto> GetFriendRequestList()
        { //burada login olan kullanıcıya istek gelmişmi onu kontrol ettiriyorum ve kim gönderdiyse isteği o kullanıcıları login olan kullanıcıya  döndürüyorum.

            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;

            var FriendRequestList = _userRepository.FriendRequests(UserId);



            var FriendRequestListMapping = _mapper.Map<List<UserDto>>(FriendRequestList);
            return FriendRequestListMapping;
        }

        public List<UserDto> GetFriendList()
        {
            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;


            var FriendList = _userRepository.FriendsOfUsers(UserId).Select(x => x.FriendOrRequestUser.Id == UserId ? x.User : x.FriendOrRequestUser);
            var FriendListMapping = _mapper.Map<List<UserDto>>(FriendList);


            return FriendListMapping;
        }

        public async Task SendFriendRequest(Guid RecevierId)
        {
            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;



            //istek listesini kontrol ediyorum istek var mı yok mu?
            var isFriendsOfUsers = await _userRepository.IsFriendsOfUsersAsync(RecevierId, UserId);
            if (isFriendsOfUsers != null)
            {
                if (isFriendsOfUsers.IsRequestAccepted)
                    throw new ClientSideException("Zaten Arkadaşsınız.");
                else
                    throw new ClientSideException($"{isFriendsOfUsers.FriendOrRequestUser.UserName}  Kullanıcısı  {isFriendsOfUsers.User.UserName}  kullanıcısına zaten istek göndermiş.");
            }

            FriendOfUsersDto friendRequestDto = new()
            {
                FriendOrRequestId = UserId,
                UserId = RecevierId,
                IsRequestAccepted = false
            };
            var friendRequstMapping = _mapper.Map<FriendsOfUser>(friendRequestDto);
            await _userRepository.SendFriendRequest(friendRequstMapping);
            await _unitOfWork.CommitAsync();
        }



        public async Task AcceptFriendRequest(Guid AcceptId)
        {//gelen arkadaşlık isteğini kabul etme.

            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;

            //arkadaş isteğini buluyorum.
            var Request = await _userRepository.GetFriendRequest(AcceptId, UserId);
            if (Request == null)
                throw new ClientSideException("İstek Bulunamadı.");

            Request.IsRequestAccepted = true;
            Request.RequestAcceptDate = DateTime.Now;
            _userRepository.AcceptFriendRequest(Request);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveFromYourFriendOrRequestList(Guid RemoveId)
        {//gelen arkadaşlık isteğini silme.
            var UserId = _getInformationFromToken.GetUserIdAndUserName().Id;
            var friendsRequest = await _userRepository.IsFriendsOfUsersAsync(RemoveId, UserId);
            if (friendsRequest == null)
                throw new ClientSideException("Böyle bir istek veya arkadaş bulunamadı.");
            _userRepository.RemoveFriendRequestOrFriend(friendsRequest);
            await _unitOfWork.CommitAsync();
        }


    }
}

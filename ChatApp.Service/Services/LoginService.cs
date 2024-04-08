using ChatApp.Core.Repositories;
using ChatApp.Core.Security;
using ChatApp.Core.Services;
using ChatApp.Core.UnifOfWorks;
using ChatApp.Entities.Entities;
using ChatApp.Service.Exceptions;
using ChatApp.Shared.DTOs;
using ChatApp.Shared.DTOs.UserDTOs;

namespace ChatApp.Service.Services
{
    public class LoginService : GenericService<User>, ILoginService
    {
        private readonly ILoginRepository _userRepository;
        private readonly IGetToken _getToken;
        private readonly IGeneratePasswordHash _generatePasswordHash;
        public LoginService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, ILoginRepository userRepository, IGetToken getToken, IGeneratePasswordHash generatePasswordHash) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _getToken = getToken;
            _generatePasswordHash = generatePasswordHash;
        }
        public async Task<Token> Login(string UserName, string Password)
        {
            var generatedPasswordHash = _generatePasswordHash.GeneratePasswordHash(Password);
            var User = await _userRepository.Login(UserName, generatedPasswordHash);

            if (User != null)
                return _getToken.CreateToken(User.Id, User.UserName, "User", User.PasswordUpdateDate);



            throw new UnauthorizedException("Kullanıcı adı veya şifre hatalı.");
        }

    }
}

using ChatApp.Core.Security;
using ChatApp.Shared.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Security
{
    public class GetToken : IGetToken
    {
        private readonly IConfiguration _configuration;
        public GetToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(Guid UserId, string UserName, string Roll, DateTime? LastUpdatePasswordDate)
        {
            Token token = new();
            //Security Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(_configuration["Token:Expiration"]));

            //Oluşturulacak token ayarlarını veriyoruz
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: new List<Claim>
                {
                    new(ClaimTypes.Name,UserName),
                    new(ClaimTypes.Role,Roll),
                    new("UserId",UserId.ToString()),
                    new(ClaimTypes.NameIdentifier,UserId.ToString()),
                    new("LastUpdatePasswordDate",LastUpdatePasswordDate.ToString())
                }
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.RefreshToken = CreateRefreshToken();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}

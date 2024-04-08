using ChatApp.Core.Security;
using ChatApp.Service.Exceptions;
using ChatApp.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service.Helpers
{
    public class GetInformationFromToken : IGetInformationFromToken
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public GetInformationFromToken(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public UserIdAndNameDto GetUserIdAndUserName()
        {
            //tokendaki kullanıcı adı ve kullanıcı id bilgilerini çekiyorum ve eğer token yoksa unauthorized hatası verecek. 
            var token = _contextAccessor?.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token.Substring("Bearer ".Length);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jsonToken = tokenHandler.ReadJwtToken(token);
                    // Eğer kullanıcı kimliği bir claim olarak token içinde mevcutsa
                    var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserId");
                    var UserName = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (userIdClaim != null && UserName != null)
                    {
                        return
                            new UserIdAndNameDto { Id = Guid.Parse(userIdClaim.Value), UserName = UserName.Value };
                    }
                    //Eğer kullanıcı kimliği claim'i bulunamazsa, hata fırlatıyoruz.
                    throw new InvalidOperationException("Token değerinde UserId ve UserName claimi bulunamadı.");
                }
            }
            throw new UnauthorizedException("Token Bulunamadı.");

        }


    }
}

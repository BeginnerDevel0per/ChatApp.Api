using ChatApp.Core.Services;
using ChatApp.Service.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatApp.Api.Middlewares
{

    public class UseCustomAuthentication
    {
        private readonly RequestDelegate _next;

        public UseCustomAuthentication(RequestDelegate next)
        {
            _next = next;

        }



        public async Task Invoke(HttpContext httpContext)
        { //burada kullanıcı şifre değişikliği yapışmı onu kontrol ediyorum.Eğer şifre değişikliği varsa tüm oturumlar sonlandırılıyor.
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {


                var _userService = httpContext.RequestServices.GetService<IUserService>();
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken parsedToken;




                parsedToken = handler.ReadJwtToken(token);
                var UserId = parsedToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
                var LastUpdateDate = parsedToken.Claims.FirstOrDefault(x => x.Type == "LastUpdatePasswordDate").Value;
                var user = await _userService.GetById(Guid.Parse(UserId));

                if (!string.IsNullOrEmpty(LastUpdateDate))
                {

                    var date = Convert.ToDateTime(LastUpdateDate).ToString("dd.MM.yyyy HH:mm");
                    var deneme = Convert.ToDateTime(user.PasswordUpdateDate).ToString("dd.MM.yyyy HH:mm");

                    if (date != deneme)
                    {
                        throw new UnauthorizedException("Geçersiz Token Değeri");
                    }

                }

            }
            await _next(httpContext);
        }



    }




}


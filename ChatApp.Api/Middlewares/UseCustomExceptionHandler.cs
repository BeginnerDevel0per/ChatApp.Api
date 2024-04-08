using ChatApp.Service.Exceptions;
using ChatApp.Shared.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ChatApp.Api.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)

        {

            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    //hatanın hangi türde fırlatılacağı
                    context.Response.ContentType = "application/json";
                    //hatayı yakalıyoruz
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();


                    var statusCode = 500;
                    //default olarak 500 hatası dönüyorum eğer exceptionFeature benim çağırdığım ClientSideException ise 404 dönecek.
                    if (exceptionFeature.Error is ClientSideException)
                    {
                        statusCode = 400;
                    }
                    if (exceptionFeature.Error is UnauthorizedException)
                    {
                        statusCode = 401;
                    }

                    context.Response.StatusCode = statusCode;

                    //sonucu aşşağıda kendi ouşturduğum  CustomResponseDto() nesnem ile döndürüyorum.
                    var response = CustomResponseDto.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });


        }
    }
}


using ChatApp.Api.Controllers;
using ChatApp.Api.Filters;
using ChatApp.Api.Middlewares;
using ChatApp.Core.StorageService;
using ChatApp.Core.Repositories;
using ChatApp.Core.Security;
using ChatApp.Core.Services;
using ChatApp.Core.UnifOfWorks;
using ChatApp.StorageService.ImageFiles;
using ChatApp.Hubs;
using ChatApp.Repository.Context;
using ChatApp.Repository.Repository;
using ChatApp.Repository.UnitOfWork;
using ChatApp.Service.Helpers;
using ChatApp.Service.Mappings;
using ChatApp.Service.Security;
using ChatApp.Service.Services;
using ChatApp.Service.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new ValidateFilterAttribute());
            });


            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateProfileDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdatePasswordDtoValidator>();



            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,

                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/ChatHub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy", policy =>
              {
                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
              });
             }
             );


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGetToken, GetToken>();
            builder.Services.AddScoped<IGetInformationFromToken, GetInformationFromToken>();
            builder.Services.AddScoped<IGeneratePasswordHash, PasswordHash>();
            builder.Services.AddScoped<IUserProfileImage, UserProfileImage>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            builder.Services.AddScoped(typeof(ILoginService), typeof(LoginService));
            builder.Services.AddScoped(typeof(ILoginRepository), typeof(LoginRepository));
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
            builder.Services.AddScoped(typeof(IMessageService), typeof(MessageService));
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
            builder.Services.AddScoped(typeof(IMessageRepository), typeof(MessageRepository));






            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(typeof(MapProfile));

            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.MapHub<ChatHub>("/ChatHub");
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseCustomException();//oluþturduðum hata fýrlatan middleware
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<UseCustomAuthentication>();
            app.MapControllers();

            app.Run();
        }
    }
}

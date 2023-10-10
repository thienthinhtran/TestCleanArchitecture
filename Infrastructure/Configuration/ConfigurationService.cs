using Data;
using Data.Abstraction;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Service;
using Service.Abstract;
using Service.Handlers;
using Service.Implementation;
using Service.Queries;
using Service.Responses;
using ServiceAuthentication;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using Service.Behaviors;
using Service.Validators;
using Service.Command;

namespace Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void RegisterContextDb(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBInit"));
            });
        }
        public static void RegisterDI(this  IServiceCollection service)
        {
            // AddScoped: mỗi khi request tới thì khởi tạo
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped<IDapperHelper, DapperHelper>();
            service.AddScoped<IBrandService, BrandService>();
            service.AddScoped<IMachineService, MachineService>(); // Replace MachineService with the actual implementation class
            //Authentication
            service.AddScoped<IUserService,  UserService>();
            service.AddScoped<ITokenHandler, ServiceAuthentication.TokenHandler>();
            service.AddScoped<IUserTokenService, UserTokenService>();
           // service.AddScoped<IPipelineBehavior<RegisterUserCommand, ErrorOr<AuthenticationDTOResponse>>, ValidationPipeline>();
        }

        public static void RegisMediatR(this IServiceCollection service)
        {
            service.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(GetAllMachineDTOQueryHandler).Assembly));
            service.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(GetAllMachineDTOQuery).Assembly));
            service.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(GetAllMachineDTOResponse).Assembly));
          //  service.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(DeleteMachineByIdCommandHandler).Assembly));

        }
        public static void RegisTokenBearer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["TokenBear:Issuer"],
                    ValidateIssuer = false,
                    ValidAudience = configuration["TokenBear:Audience"],
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenBear:SignatureKey"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                option.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var tokenHandler = context.HttpContext.RequestServices.GetRequiredService<ITokenHandler>();
                        return tokenHandler.ValidateToken(context);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        }
        public static void RegisSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Test Authorization Header (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
        public static void RegisRole(this IServiceCollection service)
        {
            service.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("UserOnly", policy =>
                    policy.RequireRole("User"));
            });
        }
        public static void RegisterFluentValidation(this IServiceCollection service)
        {
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            // service.AddTransient<IValidator<CreateProductDetailCommand>, AddProductDetailCommandValidator>();        }
            service.AddTransient<IValidator<LoginUserCommand>, LoginValidator>();
        }
    }
    
}

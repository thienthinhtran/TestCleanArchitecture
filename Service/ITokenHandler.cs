using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ServiceAuthentication
{
    public interface ITokenHandler
    {
        Task ValidateToken(TokenValidatedContext context);
        Task<(string, DateTime)> CreateAccessToken(User user);
        Task<(string, DateTime)> CreateRefreshToken(User user);
    }
}
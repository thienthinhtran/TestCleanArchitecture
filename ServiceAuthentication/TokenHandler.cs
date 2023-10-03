using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
/*using Microsoft.IdentityModel.JsonWebTokens;
*/using Microsoft.IdentityModel.Tokens;
using Service;

namespace ServiceAuthentication
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;
        IUserService _userService;

        public TokenHandler(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        public async Task<(string, DateTime)> CreateAccessToken(User user)
        {
            DateTime? expiresAt = DateTime.Now.AddMinutes(15);
            var claims = new Claim[]
            {
                //Primary key
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                // Người cấp phát ( Server )
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                // Thời gian cấp phát
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.Integer64, _configuration["TokenBear:Issuer"]),
                // Người tạo Token chính
                new Claim(JwtRegisteredClaimNames.Aud, "Thinh Web API Test", ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                // Thời gian hết hạn
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(15).ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                //new Claim(ClaimTypes.Name, user.DisplayName, ClaimValueTypes.String, _configuration["TokenBear:Issuer"])
            };

            if (!string.IsNullOrEmpty(user.UserName))
            {
                claims = claims.Concat(new Claim[] { new Claim("Username", user.UserName, ClaimValueTypes.String, "") }).ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiresAt,
                credential 
                );
            string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);
            return await Task.FromResult((accessToken, expiresAt.Value));
        }
        public async Task<(string, DateTime)> CreateRefreshToken(User user)
        {
            DateTime? expiresAt = DateTime.Now.AddMinutes(15);

            var claims = new Claim[]
            {
                //Primary key
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                // Người cấp phát ( Server )
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                // Thời gian cấp phát
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
                // Thời gian hết hạn
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddHours(3).ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiresAt,
                credential
                );
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);
            return await Task.FromResult((refreshToken, expiresAt.Value));
        }
        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims.ToList();
            if (claims.Count == 0 )
            {
                context.Fail("This token contains no information");
                return;
            }

            var identity = context.Principal.Identity as ClaimsIdentity;
            if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                context.Fail("This token is not issued by point entry");
                return;
            }
            if (identity.FindFirst("Username") != null)
            {
                string username = identity.FindFirst("Username").Value;
                var user = await _userService.FindByUsername(username);
                if (user == null)
                {
                    context.Fail($"{username} already exists.");
                    return;
                }
            }
            // check còn thời hạn k
            if(identity.FindFirst(JwtRegisteredClaimNames.Exp) == null)
            {
                var dateExp = identity.FindFirst(JwtRegisteredClaimNames.Exp).Value;

                long ticks = long.Parse(dateExp);
                var date = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;
                var minutes = date.Subtract(DateTime.Now).TotalMinutes;

                context.Fail("This token is expired");
                return;
            }
            // Record log
            // Update last time

         
        }
    }
}

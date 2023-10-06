using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Responses
{
    public class AuthenticationDTOResponse
    {
        public string AccessToken { get; set; } // JWT access token
        public DateTime AccessTokenExpiration { get; set; } // Expiration time of access token
       // public string RefreshToken { get; set; } // JWT refresh token
       // public DateTime RefreshTokenExpiration { get; set; } // Expiration time of refresh token
        public string UserName { get; set; } // User's name
        public int UserId { get; set; } // User's ID
    }
}

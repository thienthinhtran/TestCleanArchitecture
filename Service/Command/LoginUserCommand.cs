using MediatR;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command
{
    public class LoginUserCommand : IRequest<List<AuthenticationDTOResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
      //  public string Role { get; set; }
    }
}

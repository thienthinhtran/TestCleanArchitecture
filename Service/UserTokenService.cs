using Data.Abstraction;
using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IRepository<UserToken> _userTokenRepository;

        public UserTokenService(IRepository<UserToken> userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }
        public async Task SaveToken(UserToken userToken)
        {
            await _userTokenRepository.Insert(userToken);
            await _userTokenRepository.CommitAsync();
        }
    }
}

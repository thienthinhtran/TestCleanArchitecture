using Data.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<User> CheckLogin(string username, string password)
        {
            var user = await _repository.GetSingleByConditionAsync(x => x.UserName == username);
            if (user == null)
            {
                return null;
            }
            if (user.Password == password)
            {
                return user;
            }
            Console.WriteLine("Wrong password");
            return null;
        }
        public async Task<User> FindByUsername(string username)
        {
            return await _repository.GetSingleByConditionAsync(x =>x.UserName == username);
        }
    }
}

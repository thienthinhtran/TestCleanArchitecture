using Data.Abstraction;
using Domain.Entities;
using Service.Common;
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
        public async Task<UserDTO> AddUser(UserDTO userDto)
        {
            var existingUser = await FindByUsername(userDto.UserName);
            if (existingUser != null)
            {
                return null; // Handle the case where the username is already taken
            }

            // Create a new user entity and set its properties based on the DTO
            var newUser = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                Role = "User", // Set role to "User"
                /*DisplayName = null, // Set DisplayName to null
                LastLoggedIn = null, // Set LastLoggedIn to null
                CreatedDate = null // Set CreatedDate to null*/
            };

            // Add the new user to the repository
            await _repository.Insert(newUser);
            await _repository.CommitAsync();

            // Return the user DTO if needed
            return userDto;
        }

    }
}

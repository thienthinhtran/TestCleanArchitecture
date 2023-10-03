using Domain.Entities;
using Service.Common;

namespace Service
{
    public interface IUserService
    {
        Task<UserDTO> AddUser(UserDTO userDto);
        Task<User> CheckLogin(string username, string password);
        Task<User> FindByUsername(string username);
    }
}
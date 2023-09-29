using Domain.Entities;

namespace Service
{
    public interface IUserService
    {
        Task<User> CheckLogin(string username, string password);
        Task<User> FindByUsername(string username);
    }
}
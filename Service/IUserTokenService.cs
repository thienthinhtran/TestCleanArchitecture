using Domain.Entities;

namespace Service
{
    public interface IUserTokenService
    {
        Task SaveToken(UserToken userToken);
    }
}
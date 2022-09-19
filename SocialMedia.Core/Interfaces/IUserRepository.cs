using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<IReadOnlyList<User>> GetUsers();
    }
}
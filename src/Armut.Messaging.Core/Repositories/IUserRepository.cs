using Armut.Messaging.Core.Entities;
using System.Threading.Tasks;

namespace Armut.Messaging.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
    }
}

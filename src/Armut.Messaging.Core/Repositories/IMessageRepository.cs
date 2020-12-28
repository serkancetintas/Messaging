using Armut.Messaging.Core.Entities;
using System.Threading.Tasks;

namespace Armut.Messaging.Core.Repositories
{
    public interface IMessageRepository
    {
        Task SendMessageAsync(Message message);
    }
}

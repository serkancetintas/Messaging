using Armut.Messaging.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Armut.Messaging.Core.Repositories
{
    public interface IBlockUserRepository
    {
        Task<bool> IsBlockedUser(Guid blockedUserId, Guid userId);
        Task AddAsync(BlockUser blockUser);
    }
}

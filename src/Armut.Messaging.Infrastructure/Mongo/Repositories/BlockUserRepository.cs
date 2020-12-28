using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Repositories;
using Armut.Messaging.Infrastructure.Mongo.Documents;
using System;
using System.Threading.Tasks;

namespace Armut.Messaging.Infrastructure.Mongo.Repositories
{
    public class BlockUserRepository : IBlockUserRepository
    {
        private readonly IMongoRepository<BlockUserDocument, Guid> _repository;
        public BlockUserRepository(IMongoRepository<BlockUserDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsBlockedUser(Guid blockedUserId, Guid userId)
        {
            var result = await _repository.ExistsAsync(x => (x.UserId == blockedUserId && x.BlockedUserId == userId) ||
                                                            (x.UserId == userId && x.BlockedUserId == blockedUserId));

            return result;
        }

        public Task AddAsync(BlockUser blockUser) => _repository.AddAsync(blockUser.AsDocument());

    }
}

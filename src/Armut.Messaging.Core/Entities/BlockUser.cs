using Armut.Messaging.Core.Exceptions;
using System;

namespace Armut.Messaging.Core.Entities
{
    public class BlockUser : AggregateRoot
    {
        public Guid BlockedUserId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public BlockUser(Guid id, Guid blockedUserId, Guid userId, DateTime createdAt)
        {
            if (Guid.Empty == blockedUserId)
            {
                throw new InvalidBlockedUserException();
            }

            if (Guid.Empty == userId)
            {
                throw new InvalidUserException();
            }

            Id = id;
            BlockedUserId = blockedUserId;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}

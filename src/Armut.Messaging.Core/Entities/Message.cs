using Armut.Messaging.Core.Exceptions;
using System;

namespace Armut.Messaging.Core.Entities
{
    public class Message : AggregateRoot
    {
        public Guid FromUserId { get; private set; }
        public Guid ToUserId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Message(Guid id, Guid fromUserId, Guid toUserId, string content, DateTime createdAt)
        {
            if (Guid.Empty == toUserId)
            {
                throw new InvalidToUserException();
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidContentException();
            }

            if (Guid.Empty == fromUserId)
            {
                throw new InvalidFromUserException();
            }

            Id = id;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}

using Armut.Messaging.Infrastructure.Types;
using System;

namespace Armut.Messaging.Infrastructure.Mongo.Documents
{
    [BsonCollection("messages")]
    public class BlockUserDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid BlockedUserId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

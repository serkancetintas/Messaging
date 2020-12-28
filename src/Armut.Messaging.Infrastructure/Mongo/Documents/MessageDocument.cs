using Armut.Messaging.Infrastructure.Types;
using System;

namespace Armut.Messaging.Infrastructure.Mongo.Documents
{
    [BsonCollection("messages")]
    public class MessageDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

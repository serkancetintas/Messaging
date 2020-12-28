using Armut.Messaging.Infrastructure.Types;
using System;

namespace Armut.Messaging.Infrastructure.Mongo.Documents
{
    [BsonCollection("users")]
    public class UserDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

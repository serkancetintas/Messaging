using Armut.Messaging.Core.Entities;

namespace Armut.Messaging.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static User AsEntity(this UserDocument document)
          => new User(document.Id, document.Email, document.UserName, document.Password, document.CreatedAt);


        public static UserDocument AsDocument(this User entity)
            => new UserDocument
            {
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                Password = entity.Password,
                CreatedAt = entity.CreatedAt
            };

        public static Message AsEntity(this MessageDocument document)
         => new Message(document.Id, document.FromUserId, document.ToUserId, document.Content, document.CreatedAt);

        public static MessageDocument AsDocument(this Message entity)
           => new MessageDocument
           {
               Id = entity.Id,
               FromUserId = entity.FromUserId,
               ToUserId = entity.ToUserId,
               Content = entity.Content,
               CreatedAt = entity.CreatedAt
           };

        public static BlockUser AsEntity(this BlockUserDocument document)
        => new BlockUser(document.Id, document.BlockedUserId, document.UserId, document.CreatedAt);

        public static BlockUserDocument AsDocument(this BlockUser entity)
          => new BlockUserDocument
          {
              Id = entity.Id,
              BlockedUserId = entity.BlockedUserId,
              UserId = entity.UserId,
              CreatedAt = entity.CreatedAt
          };
    }
}

using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Repositories;
using Armut.Messaging.Infrastructure.Mongo.Documents;
using System;
using System.Threading.Tasks;

namespace Armut.Messaging.Infrastructure.Mongo.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoRepository<MessageDocument, Guid> _repository;
        public MessageRepository(IMongoRepository<MessageDocument, Guid> repository)
        {
            _repository = repository;
        }

        public Task SendMessageAsync(Message message) => _repository.AddAsync(message.AsDocument());
    }
}

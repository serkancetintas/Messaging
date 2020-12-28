using Armut.Messaging.Application;
using Armut.Messaging.Application.DTO;
using Armut.Messaging.Application.Queries;
using Armut.Messaging.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Armut.Messaging.Infrastructure.Mongo.Queries.Handlers
{
    public class GetMessagesHandler:IQueryHandler<GetMessages,IEnumerable<MessageDto>>
    {
        private readonly IMongoRepository<MessageDocument, Guid> _messageRepository;
        private readonly IMongoRepository<UserDocument, Guid> _userRepository;
        private readonly IUserContext _userContext;
        public GetMessagesHandler(IMongoRepository<MessageDocument, Guid> messageRepository,
                                  IMongoRepository<UserDocument, Guid> userRepository,
                                  IUserContext userContext)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<IEnumerable<MessageDto>> HandleAsync(GetMessages query)
        {
            var user = await _userRepository.GetAsync(p => p.UserName == query.FromUserName);
            var myUser = await _userRepository.GetAsync(_userContext.Id);

            var result = await _messageRepository
                .FindAndSortByAsync(x => (x.FromUserId == user.Id && x.ToUserId == _userContext.Id) ||
                                         (x.FromUserId == _userContext.Id && x.ToUserId == user.Id), x => x.CreatedAt);

            return result.Select(p => new MessageDto
            {
                Id = p.Id,
                FromUserName = user.UserName,
                MyUserName = myUser.UserName,
                Content = p.Content,
                CreatedAt = p.CreatedAt
            });
        }

    }
}

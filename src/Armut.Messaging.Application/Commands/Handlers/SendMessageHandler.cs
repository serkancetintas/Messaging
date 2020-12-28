using Armut.Messaging.Application.Exceptions;
using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Armut.Messaging.Application.Commands.Handlers
{
    public class SendMessageHandler: ICommandHandler<SendMessage>
    {
        private readonly IMessageRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IBlockUserRepository _blockUserRepository;
        private readonly IUserContext _userContext;
        private readonly ILogger<SendMessageHandler> _logger;
        public SendMessageHandler(IMessageRepository repository,
                                  IUserRepository userRepository,
                                  IBlockUserRepository blockUserRepository,
                                  IUserContext userContext,
                                  ILogger<SendMessageHandler> logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _blockUserRepository = blockUserRepository;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task HandleAsync(SendMessage command)
        {
            var toUser = await _userRepository.GetByUserNameAsync(command.ToUserName);
            if (toUser is null)
            {
                _logger.LogError($"User not found: {command.ToUserName}");
                throw new UserNotFoundException(command.ToUserName);
            }

            var isBlokedUser = await _blockUserRepository.IsBlockedUser(toUser.Id, _userContext.Id);
            if (isBlokedUser)
            {
                _logger.LogError($" User with id: {_userContext.Id}. Blocked user : {toUser.UserName}");
                throw new BlockedUserException(toUser.UserName);
            }

            var message = new Message(Guid.NewGuid(), _userContext.Id, toUser.Id, command.Content, DateTime.Now);

            await _repository.SendMessageAsync(message);
        }
    }
}

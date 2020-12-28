using Armut.Messaging.Application.Exceptions;
using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Armut.Messaging.Application.Commands.Handlers
{
    public class BlockUserHandler : ICommandHandler<Block>
    {
        private readonly IBlockUserRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly ILogger<BlockUserHandler> _logger;
        public BlockUserHandler(IBlockUserRepository repository,
                                IUserRepository userRepository,
                                IUserContext userContext,
                                ILogger<BlockUserHandler> logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _userContext = userContext;
            _logger = logger;
        }

        public async Task HandleAsync(Block command)
        {
            var blockedUser = await _userRepository.GetByUserNameAsync(command.UserName);
            if (blockedUser is null)
            {
                _logger.LogError($"User not found: {command.UserName}");
                throw new UserNotFoundException(command.UserName);
            }

            var blockUser = new BlockUser(Guid.NewGuid(), blockedUser.Id, _userContext.Id, DateTime.Now);

            await _repository.AddAsync(blockUser);
        }
    }
}

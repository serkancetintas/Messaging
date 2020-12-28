using Armut.Messaging.Application;
using Armut.Messaging.Application.Commands;
using Armut.Messaging.Application.Commands.Handlers;
using Armut.Messaging.Application.Exceptions;
using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Repositories;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Armut.Messaging.Tests.Unit.Applications.Handlers
{
    public class SendMessageHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task given_invalid_username_send_message_should_throw_an_exception
            ([Frozen] Mock<IUserRepository> userRepository,
             SendMessageHandler handler)
        {
            var sendMessage = new SendMessage("", "test");
            User user = null;
            userRepository.Setup(r => r.GetByUserNameAsync("")).ReturnsAsync(user);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(sendMessage));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<UserNotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task given_blocked_user_id_send_message_should_throw_an_exception
           ([Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IUserContext> userContext,
            [Frozen] Mock<IBlockUserRepository> blockUserRepository,
            User toUser,
            SendMessageHandler handler)
        {
            var sendMessage = new SendMessage("", "test");
            userRepository.Setup(r => r.GetByUserNameAsync("")).ReturnsAsync(toUser);
            blockUserRepository.Setup(r => r.IsBlockedUser(toUser.Id, userContext.Object.Id)).ReturnsAsync(true);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(sendMessage));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<BlockedUserException>();
        }

        [Theory, AutoMoqData]
        public async Task given_valid_parameters_send_message_should_success
           ([Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IUserContext> userContext,
            [Frozen] Mock<IBlockUserRepository> blockUserRepository,
            User toUser,
            SendMessageHandler handler)
        {
            var sendMessage = new SendMessage(toUser.UserName, "test");

            userRepository.Setup(r => r.GetByUserNameAsync(toUser.UserName)).ReturnsAsync(toUser);
            blockUserRepository.Setup(r => r.IsBlockedUser(toUser.Id, userContext.Object.Id)).ReturnsAsync(false);
            userContext.Setup(r => r.Id).Returns(Guid.NewGuid());
            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(sendMessage));

            exception.Should().BeNull();
        }
    }


}

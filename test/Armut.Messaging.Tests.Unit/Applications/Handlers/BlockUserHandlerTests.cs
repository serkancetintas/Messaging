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
    public class BlockUserHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task given_invalid_username_block_user_should_throw_an_exception
            ([Frozen] Mock<IUserRepository> userRepository,
             BlockUserHandler handler)
        {
            var block = new Block("");
            User user = null;
            userRepository.Setup(r => r.GetByUserNameAsync("")).ReturnsAsync(user);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(block));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<UserNotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task given_valid_parameters_block_user_should_success
           ([Frozen] Mock<IUserRepository> userRepository,
            [Frozen] Mock<IUserContext> userContext,
            User blockUser,
            BlockUserHandler handler)
        {
            var block = new Block(blockUser.UserName);
            userRepository.Setup(r => r.GetByUserNameAsync(blockUser.UserName)).ReturnsAsync(blockUser);
            userContext.Setup(r => r.Id).Returns(Guid.NewGuid());

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(block));

            exception.Should().BeNull();
        }
    }
}

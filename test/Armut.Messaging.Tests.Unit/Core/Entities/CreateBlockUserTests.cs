using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Armut.Messaging.Tests.Unit.Core.Entities
{
    public class CreateBlockUserTests
    {
        private BlockUser Act(AggregateId id, Guid blockUserId, Guid userId, DateTime createdAt) => new BlockUser(id, blockUserId, userId, createdAt);

        [Fact]
        public void given_valid_params_blockuser_should_be_created()
        {
            // Arrange
            var id = new AggregateId();
            var blockUserId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var createdAt = DateTime.Now;

            //Act
            var blockUser = Act(id, blockUserId, userId, createdAt);

            //Assert
            blockUser.Should().NotBeNull();
            blockUser.Id.Should().Be(id);
            blockUser.BlockedUserId.Should().Be(blockUserId);
            blockUser.UserId.Should().Be(userId);
            blockUser.CreatedAt.Should().Be(createdAt);
        }

        [Fact]
        public void given_empty_blockuserid_blockuser_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            var blockUserId = Guid.Empty;
            var userId = Guid.NewGuid();
            var createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, blockUserId, userId, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidBlockedUserException>();
        }

        [Fact]
        public void given_empty_userid_blockuser_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            var blockUserId = Guid.NewGuid();
            var userId = Guid.Empty;
            var createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, blockUserId, userId, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidUserException>();
        }


    }
}

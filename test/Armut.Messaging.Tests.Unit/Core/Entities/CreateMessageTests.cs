using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Armut.Messaging.Tests.Unit.Core.Entities
{
    public class CreateMessageTests
    {
        private Message Act(AggregateId id, Guid fromUserId, Guid toUserId, string content, DateTime createdAt) => new Message(id, fromUserId, toUserId, content, createdAt);

        [Fact]
        public void given_valid_params_message_should_be_created()
        {
            // Arrange
            var id = new AggregateId();
            Guid fromUserId = Guid.NewGuid();
            Guid toUserId = Guid.NewGuid();
            string content = "selam";
            DateTime createdAt = DateTime.Now;

            //Act
            var message = Act(id, fromUserId, toUserId, content, createdAt);

            //Assert
            message.Should().NotBeNull();
            message.Id.Should().Be(id);
            message.FromUserId.Should().Be(fromUserId);
            message.ToUserId.Should().Be(toUserId);
            message.Content.Should().Be(content);
            message.CreatedAt.Should().Be(createdAt);
        }

        [Fact]
        public void given_empty_fromuser_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            Guid fromUserId = Guid.Empty;
            Guid toUserId = Guid.NewGuid();
            string content = "selam";
            DateTime createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, fromUserId, toUserId, content, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidFromUserException>();
        }

        [Fact]
        public void given_empty_touser_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            Guid fromUserId = Guid.NewGuid();
            Guid toUserId = Guid.Empty;
            string content = "selam";
            DateTime createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, fromUserId, toUserId, content, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidToUserException>();
        }

        [Fact]
        public void given_empty_content_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            Guid fromUserId = Guid.NewGuid();
            Guid toUserId = Guid.NewGuid();
            string content = string.Empty;
            DateTime createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, fromUserId, toUserId, content, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidContentException>();
        }
    }
}

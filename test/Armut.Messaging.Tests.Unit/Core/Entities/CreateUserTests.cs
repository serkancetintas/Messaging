using Armut.Messaging.Core.Entities;
using Armut.Messaging.Core.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace Armut.Messaging.Tests.Unit.Core.Entities
{
    public class CreateUserTests
    {
        private User Act(AggregateId id, string email, string userName, string password, DateTime createdAt) => new User(id, email, userName, password, createdAt);

        [Fact]
        public void given_valid_params_user_should_be_created()
        {
            // Arrange
            var id = new AggregateId();
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = "abRcdf14";
            DateTime createdAt = DateTime.Now;

            //Act
            var user = Act(id, email, userName, password, createdAt);

            //Assert
            user.Should().NotBeNull();
            user.Id.Should().Be(id);
            user.Email.Should().Be(email);
            user.UserName.Should().Be(userName);
            user.Password.Should().Be(password);
            user.CreatedAt.Should().Be(createdAt);
        }

        [Fact]
        public void given_empty_email_user_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string email = string.Empty;
            string userName = "testuser";
            string password = "abRcdf14";
            DateTime createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, email, userName, password, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidEmailException>();
        }

        [Fact]
        public void given_empty_username_user_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string email = "test@gmail.com";
            string userName = string.Empty;
            string password = "abRcdf14";
            DateTime createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, email, userName, password, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidUserNameException>();
        }

        [Fact]
        public void given_empty_password_user_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string email = "test@gmail.com";
            string userName = "testuser";
            string password = string.Empty;
            DateTime createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, email, userName, password, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidPasswordException>();
        }


    }
}

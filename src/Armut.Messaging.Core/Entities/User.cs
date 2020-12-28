using Armut.Messaging.Core.Exceptions;
using System;

namespace Armut.Messaging.Core.Entities
{
    public class User: AggregateRoot
    {
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User(Guid id, string email, string userName, string password, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException(email);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new InvalidUserNameException(userName);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidPasswordException();
            }

            Id = id;
            Email = email.ToLowerInvariant();
            UserName = userName.ToLowerInvariant();
            Password = password;
            CreatedAt = createdAt;
        }

    }
}

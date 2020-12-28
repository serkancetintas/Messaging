namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidUserNameException: DomainException
    {
        public override string Code { get; } = "invalid_username";
        public InvalidUserNameException(string userName) : base(message: $"Invalid user name: {userName}." +
            $" Your username must be a minimum of 3 and a maximum of 15 characters. " +
            $"It must start with a letter and not contain special characters.")
        {
        }
    }
}

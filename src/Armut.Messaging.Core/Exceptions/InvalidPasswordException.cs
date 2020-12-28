namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidPasswordException : DomainException
    {
        public override string Code { get; } = "invalid_password";

        public InvalidPasswordException() : base($"Invalid password. " +
            $"Your password must contain at least one uppercase letter and a number. Must have a minimum of 8 characters")
        {
        }
    }
}

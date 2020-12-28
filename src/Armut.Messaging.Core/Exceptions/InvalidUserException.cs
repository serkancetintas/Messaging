namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidUserException : DomainException
    {
        public override string Code { get; } = "invalid_user";
        public InvalidUserException() : base("Invalid user.")
        {
        }
    }
}

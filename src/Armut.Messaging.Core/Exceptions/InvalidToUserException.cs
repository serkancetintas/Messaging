namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidToUserException : DomainException
    {
        public override string Code { get; } = "invalid_to_user";
        public InvalidToUserException() : base("Invalid to user.")
        {
        }
    }
}

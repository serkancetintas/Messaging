namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidBlockedUserException: DomainException
    {
        public override string Code { get; } = "invalid_blocked_user";
        public InvalidBlockedUserException() : base("Invalid blocked user.")
        {
        }
    }
}

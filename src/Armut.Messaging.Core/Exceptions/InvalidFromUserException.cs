namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidFromUserException : DomainException
    {
        public override string Code { get; } = "invalid_from_user";
        public InvalidFromUserException() : base("Invalid from user.")
        {
        }
    }
}

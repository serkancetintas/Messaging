namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidContentException : DomainException
    {
        public override string Code { get; } = "invalid_content";

        public InvalidContentException() : base("Invalid content.")
        {
        }
    }
}

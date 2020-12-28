namespace Armut.Messaging.Core.Exceptions
{
    public class InvalidCredentialsException : DomainException
    {
        public override string Code { get; } = "invalid_credentials";
        public string UserName { get; }

        public InvalidCredentialsException(string userName) : base("Invalid credentials.")
        {
            UserName = userName;
        }
    }
}

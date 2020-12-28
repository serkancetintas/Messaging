namespace Armut.Messaging.Core.Exceptions
{
    public class UserNameInUseException : DomainException
    {
        public override string Code { get; } = "username_in_use";
        public string UserName { get; }

        public UserNameInUseException(string userName) : base($"User name {userName} is already in use.")
        {
            UserName = userName;
        }
    }
}

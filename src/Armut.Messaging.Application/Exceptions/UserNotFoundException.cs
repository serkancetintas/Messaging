namespace Armut.Messaging.Application.Exceptions
{
    public class UserNotFoundException: AppException
    {
        public override string Code { get; } = "user_not_found";
        public string UserName{ get; }

        public UserNotFoundException(string userName) : base($"User with name: '{userName}' was not found.")
        {
            UserName = userName;
        }
    }
}

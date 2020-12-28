namespace Armut.Messaging.Application.Exceptions
{
    public class BlockedUserException:AppException
    {
        public override string Code { get; } = "blocked_user";
        public string UserName { get; }

        public BlockedUserException(string userName) : base($"You are blocked by: {userName}.")
            => UserName = userName;
    }
}

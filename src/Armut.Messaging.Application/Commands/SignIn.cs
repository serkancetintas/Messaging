namespace Armut.Messaging.Application.Commands
{
    public class SignIn: ICommand
    {
        public string UserName { get; }
        public string Password { get; }

        public SignIn(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}

namespace Armut.Messaging.Application.Commands
{
    public class SignUp:ICommand
    {
        public string Email { get; }
        public string UserName { get; }
        public string Password { get; }

        public SignUp(string email, string userName, string password)
        {
            Email = email;
            UserName = userName;
            Password = password;
        }
    }
}

namespace Armut.Messaging.Application.Commands
{
    public class Block : ICommand
    {
        public string UserName { get; }

        public Block(string userName)
        {
            UserName = userName;
        }
    }
}

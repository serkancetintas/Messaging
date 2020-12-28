namespace Armut.Messaging.Application.Commands
{
    public class SendMessage: ICommand
    {
        public string ToUserName { get; }
        public string Content { get; }

        public SendMessage(string toUserName, string content)
        {
            ToUserName = toUserName;
            Content = content;
        }
    }
}

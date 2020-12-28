namespace Armut.Messaging.Infrastructure.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}

namespace Armut.Messaging.Core.Entities
{
    public abstract class AggregateRoot
    {
        public AggregateId Id { get; protected set; }
    }
}

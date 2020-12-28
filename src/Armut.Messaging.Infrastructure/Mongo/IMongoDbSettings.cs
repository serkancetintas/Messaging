namespace Armut.Messaging.Infrastructure.Mongo
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}

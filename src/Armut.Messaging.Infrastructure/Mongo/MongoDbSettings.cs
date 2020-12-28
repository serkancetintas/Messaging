namespace Armut.Messaging.Infrastructure.Mongo
{
    public class MongoDbSettings: IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}

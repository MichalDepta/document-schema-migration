using MongoDB.Driver;

namespace DocumentSchemaMigration.DataAccess
{
    public class CollectionFactory
    {
        private const string DbName = "DocumentSchemaMigration";

        private readonly IMongoClient dbClient;

        public CollectionFactory()
        {
            this.dbClient = new MongoClient("mongodb://localhost:27017");
        }

        public IMongoCollection<TDocument> Create<TDocument>(string collectionName) => dbClient.GetDatabase(DbName)
            .GetCollection<TDocument>(collectionName);
    }
}
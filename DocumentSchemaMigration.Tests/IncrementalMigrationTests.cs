using System.Threading.Tasks;
using DocumentSchemaMigration.DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace DocumentSchemaMigration.Tests
{
    public class IncrementalMigrationTests
    {
        private const string CollectionName = "Rockstars";
        private readonly CollectionFactory collectionFactory = new CollectionFactory();

        
        public IncrementalMigrationTests()
        {
            MappingConfiguration.Initialize();
            var collection = this.collectionFactory.Create<BsonDocument>(CollectionName);
            collection.DeleteMany(Builders<BsonDocument>.Filter.Empty);
        }

        [Fact]
        public async Task ShouldReadV1Document()
        {
            var collection = this.collectionFactory.Create<Models.v1.Musician>(CollectionName);
            await collection.InsertManyAsync(RockstarFactory.CreateV1());

            var rockstars = await collection.Find(x => true).ToListAsync();

            Assert.NotEmpty(rockstars);
        }
    }
}
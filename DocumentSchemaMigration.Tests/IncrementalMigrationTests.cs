using System.Collections;
using System.Collections.Generic;
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
            await PopulateRockstars(RockstarFactory.CreateV1());

            var v1Collection = this.collectionFactory.Create<Models.v1.Musician>(CollectionName);
            var rockstars = await v1Collection.Find(x => true).ToListAsync();

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        public async Task ShouldReadV2Document()
        {
            await PopulateRockstars(RockstarFactory.CreateV2());

            var v2Collection = this.collectionFactory.Create<Models.v2.Musician>(CollectionName);
            var rockstars = await v2Collection.Find(x => true).ToListAsync();

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        public async Task ShouldUpgradeV1DocumentToV2()
        {
            await PopulateRockstars(RockstarFactory.CreateV1());

            var v2Collection = this.collectionFactory.Create<Models.v2.Musician>(CollectionName);
            var rockstars = await v2Collection.Find(x => true).ToListAsync();

            Assert.NotEmpty(rockstars);
            Assert.All(rockstars, musician => Assert.NotNull(musician.Bands));
        }

        private Task PopulateRockstars<TRockstar>(IEnumerable<TRockstar> rockstars) =>
            this.collectionFactory.Create<TRockstar>(CollectionName).InsertManyAsync(rockstars);
    }
}
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
            
            var rockstars = await ReadAll<Models.v1.Musician>();

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        public async Task ShouldReadV2Document()
        {
            await PopulateRockstars(RockstarFactory.CreateV2());

            var rockstars = await ReadAll<Models.v2.Musician>();

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        public async Task ShouldUpgradeV1DocumentToV2()
        {
            await PopulateRockstars(RockstarFactory.CreateV1());

            var rockstars = await ReadAll<Models.v2.Musician>();

            Assert.NotEmpty(rockstars);
            Assert.All(rockstars, musician => Assert.NotNull(musician.Bands));
        }

        [Fact]
        private async Task ShouldReadV3Document()
        {
            await PopulateRockstars(RockstarFactory.CreateV3());

            var rockstars = await ReadAll<Models.v3.Musician>();

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        private async Task ShouldUpgradeV2DocumentToV3()
        {
            await PopulateRockstars(RockstarFactory.CreateV2());

            var rockstars = await ReadAll<Models.v3.Musician>();

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        private async Task ShouldUpgradeV1DocumentToV3()
        {
            await PopulateRockstars(RockstarFactory.CreateV1());

            var rockstars = await ReadAll<Models.v3.Musician>();

            Assert.NotEmpty(rockstars);
        }

        private Task PopulateRockstars<TRockstar>(IEnumerable<TRockstar> rockstars) =>
            this.collectionFactory.Create<TRockstar>(CollectionName).InsertManyAsync(rockstars);

        private async Task<IReadOnlyList<TRockstar>> ReadAll<TRockstar>() =>
            await this.collectionFactory.Create<TRockstar>(CollectionName).Find(_ => true).ToListAsync();
    }
}
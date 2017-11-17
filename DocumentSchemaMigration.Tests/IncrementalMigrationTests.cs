using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private static readonly Dictionary<int, Func<IEnumerable>> VersionedDataFactories = new Dictionary<int, Func<IEnumerable>>
        {
            [1] = RockstarFactory.CreateV1,
            [2] = RockstarFactory.CreateV2,
            [3] = RockstarFactory.CreateV3,
            [4] = RockstarFactory.CreateV4,
        };

        private static readonly Dictionary<int, Func<IncrementalMigrationTests, Task<IEnumerable>>> VersionedDataReaders = new Dictionary<int, Func<IncrementalMigrationTests, Task<IEnumerable>>>
        {
            [1] = async x => await x.ReadAll<Models.v1.Musician>(),
            [2] = async x => await x.ReadAll<Models.v2.Musician>(),
            [3] = async x => await x.ReadAll<Models.v3.Musician>(),
            [4] = async x => await x.ReadAll<Models.v4.Musician>(),
        };

        public IncrementalMigrationTests()
        {
            MappingConfiguration.Initialize();
            var collection = this.collectionFactory.Create<BsonDocument>(CollectionName);
            collection.DeleteMany(Builders<BsonDocument>.Filter.Empty);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 3)]
        [InlineData(3, 3)]
        [InlineData(1, 4)]
        [InlineData(2, 4)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        public async Task ShouldMigrateDocuments(int sourceVersion, int targetVersion)
        {
            await PopulateRockstars(VersionedDataFactories[sourceVersion]().OfType<object>());

            var rockstars = await VersionedDataReaders[targetVersion](this);

            Assert.NotEmpty(rockstars);
        }

        [Fact]
        public async Task ShouldSetDefaultV2MemberValue()
        {
            await PopulateRockstars(RockstarFactory.CreateV1());

            var rockstars = await ReadAll<Models.v2.Musician>();

            Assert.All(rockstars, musician => Assert.NotNull(musician.Bands));
        }

        [Fact]
        public async Task ShouldConvertV3ScalarFieldToV4Array()
        {
            await PopulateRockstars(RockstarFactory.CreateV3());

            var rockstars = await ReadAll<Models.v4.Musician>();

            Assert.NotEmpty(rockstars);
            Assert.All(rockstars, x => Assert.NotEmpty(x.Instruments));
        }

        private Task PopulateRockstars<TRockstar>(IEnumerable<TRockstar> rockstars) =>
            this.collectionFactory.Create<TRockstar>(CollectionName).InsertManyAsync(rockstars);

        private async Task<IReadOnlyList<TRockstar>> ReadAll<TRockstar>() =>
            await this.collectionFactory.Create<TRockstar>(CollectionName).Find(_ => true).ToListAsync();
    }
}
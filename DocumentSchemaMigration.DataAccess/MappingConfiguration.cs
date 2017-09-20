using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace DocumentSchemaMigration.DataAccess
{
    public class MappingConfiguration
    {
        public static void Initialize()
        {
            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new NamedIdMemberConvention("Id"),
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("My Convention", conventionPack,
                type => type.FullName.StartsWith("DocumentSchemaMigration.Models"));

            BsonClassMap.RegisterClassMap<Models.v1.Musician>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
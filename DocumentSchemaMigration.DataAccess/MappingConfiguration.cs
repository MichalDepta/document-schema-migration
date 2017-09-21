using System;
using System.Linq;
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

            if (!BsonClassMap.IsClassMapRegistered(typeof(Models.v2.Musician)))
            {
                BsonClassMap.RegisterClassMap<Models.v2.Musician>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(m => m.Bands).SetDefaultValue(Enumerable.Empty<string>);
                    cm.MapMember(m => m.Birthdate).SetDefaultValue(DateTime.MinValue);
                });
            }
        }
    }
}
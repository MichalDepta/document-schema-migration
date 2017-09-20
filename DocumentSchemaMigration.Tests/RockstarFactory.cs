using System;
using System.Collections.Generic;
using System.Linq;
using DocumentSchemaMigration.Models.v1;

namespace DocumentSchemaMigration.Tests
{
    public class RockstarFactory
    {
        public static IEnumerable<Models.v1.Musician> CreateV1() => new[]
        {
            new Musician(Guid.NewGuid().ToString(), "Ritchie", "Blackmore", Intrument.Guitar),
            new Musician(Guid.NewGuid().ToString(), "Roger", "Glover", Intrument.Bass)
        }.AsEnumerable();
    }
}
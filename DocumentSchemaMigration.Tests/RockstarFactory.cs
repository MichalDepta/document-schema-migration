using System;
using System.Collections.Generic;
using System.Linq;
using DocumentSchemaMigration.Models;

namespace DocumentSchemaMigration.Tests
{
    public class RockstarFactory
    {
        public static IEnumerable<Models.v1.Musician> CreateV1() => new[]
        {
            new Models.v1.Musician(NewId(), "Ritchie", "Blackmore", Instrument.Guitar),
            new Models.v1.Musician(NewId(), "Roger", "Glover", Instrument.Bass)
        }.AsEnumerable();

        public static IEnumerable<Models.v2.Musician> CreateV2() => new[]
        {
            new Models.v2.Musician(NewId(), "Ian", "Gillan", Instrument.Vocals, new DateTime(1945, 8, 19), new[] {"Deep Purple"}),
            new Models.v2.Musician(NewId(), "Jimmy", "Page", Instrument.Guitar, new DateTime(1944, 1, 9), new [] {"Led Zeppelin"})
        }.AsEnumerable();

        public static IEnumerable<Models.v3.Musician> CreateV3() => new[]
        {
            new Models.v3.Musician(NewId(), "Tony", "Iommi", Instrument.Guitar, new[] {"Black Sabbath"}),
            new Models.v3.Musician(NewId(), "John", "Bonham", Instrument.Drums, new[] {"Led Zeppelin"})
        }.AsEnumerable();

        private static string NewId() => Guid.NewGuid().ToString();
    }
}
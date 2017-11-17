using System.Collections.Generic;

namespace DocumentSchemaMigration.Models.v3
{
    public class Musician : IVersioned
    {
        public Musician(string id, string firstName, string lastName, Instrument instrument, IEnumerable<string> bands, int version = 3)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Instrument = instrument;
            Bands = bands;
            Version = version;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Instrument Instrument { get; }

        // Removed Birthday field
        
        public IEnumerable<string> Bands { get; }
        public int Version { get; }
    }
}
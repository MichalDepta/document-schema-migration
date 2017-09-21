using System.Collections.Generic;

namespace DocumentSchemaMigration.Models.v3
{
    public class Musician
    {
        public Musician(string id, string firstName, string lastName, Instrument instrument, IEnumerable<string> bands)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Instrument = instrument;
            Bands = bands;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Instrument Instrument { get; }

        // Removed Birthday field
        
        public IEnumerable<string> Bands { get; }
    }
}
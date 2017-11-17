using System.Collections.Generic;
using System.ComponentModel;

namespace DocumentSchemaMigration.Models.v4
{
    public class Musician : ISupportInitialize, IVersioned
    {
        public Musician(string id, string firstName, string lastName, IEnumerable<string> bands, IEnumerable<Instrument> instruments, int version = 4)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Bands = bands;
            Instruments = instruments;
            Version = version;
        }

        public string Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public IEnumerable<string> Bands { get; private set; }

        // Musician can play many instruments
        public IEnumerable<Instrument> Instruments { get; private set; }

        // Use extra elements to deserialise legacy fields
        public IDictionary<string, object> ExtraElements { get; private set; }

        public int Version { get; private set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            if (ExtraElements == null) return;

            const string oldIntrumentFieldName = "instrument";
            if (ExtraElements.TryGetValue(oldIntrumentFieldName, out var instrument))
            {
                ExtraElements.Remove(oldIntrumentFieldName);
                this.Instruments = new[] { (Instrument)instrument };
            }
        }
    }
}
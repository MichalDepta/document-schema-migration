using System.Collections.Generic;
using System.ComponentModel;

namespace DocumentSchemaMigration.Models.v4
{
    public class Musician : ISupportInitialize
    {
        public Musician(string id, string firstName, string lastName, IEnumerable<string> bands, IEnumerable<Instrument> instruments)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Bands = bands;
            Instruments = instruments;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public IEnumerable<string> Bands { get; }

        // Musician can play many instruments
        public IEnumerable<Instrument> Instruments { get; private set; }

        public IDictionary<string, object> ExtraElements { get; private set; }

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
                this.Instruments = new[] {(Instrument) instrument};
            }
        }
    }
}
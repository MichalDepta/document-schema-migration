namespace DocumentSchemaMigration.Models.v1
{
    public class Musician : IVersioned
    {
        public Musician(string id, string firstName, string lastName, Instrument instrument, int version = 1)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Instrument = instrument;
            Version = version;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Instrument Instrument { get; }

        public int Version { get; }
    }
}

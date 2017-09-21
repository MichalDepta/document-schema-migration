namespace DocumentSchemaMigration.Models.v1
{
    public class Musician
    {
        public Musician(string id, string firstName, string lastName, Instrument instrument)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Instrument = instrument;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Instrument Instrument { get; }
    }
}

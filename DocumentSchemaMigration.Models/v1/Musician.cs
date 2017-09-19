namespace DocumentSchemaMigration.Models.v1
{
    public class Musician
    {
        public Musician(string id, string firstName, string lastName, Intrument intrument)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Intrument = intrument;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Intrument Intrument { get; }
    }

    public enum Intrument
    {
        Vocals,
        Guiter,
        Bass,
        Drums,
        Keyboard
    }
}

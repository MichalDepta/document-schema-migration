using System;
using System.Collections.Generic;

namespace DocumentSchemaMigration.Models.v2
{
    public class Musician
    {
        public Musician(string id, string firstName, string lastName, Instrument instrument, DateTime birthdate, IEnumerable<string> bands)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Instrument = instrument;
            Birthdate = birthdate;
            Bands = bands;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Instrument Instrument { get; }

        // New field
        public DateTime Birthdate { get; }

        // New array field
        public IEnumerable<string> Bands { get; }
    }
}
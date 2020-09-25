using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M.Challenge.Read.Domain.Entities
{
    public class Person
    {
        [Key]
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ethnicity { get; set; }
        public string Genre { get; set; }
        public List<Person> Filiation { get; set; }
        public List<Person> Children { get; set; }
        public string EducationLevel { get; set; }

        public Person(string name,
            string lastName,
            string ethnicity,
            string genre,
            string educationLevel)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Ethnicity = ethnicity ?? throw new ArgumentNullException(nameof(ethnicity));
            Genre = genre ?? throw new ArgumentNullException(nameof(genre));
            EducationLevel = educationLevel ?? throw new ArgumentNullException(nameof(educationLevel));

            Filiation = new List<Person>();
            Children = new List<Person>();
        }

        public Person AddFilliation(Person fatherOrMother)
        {
            Filiation.Add(fatherOrMother);

            return this;
        }

        public Person AddChild(Person sonOrDaughter)
        {
            Children.Add(sonOrDaughter);

            return this;
        }
    }
}

using M.Challenge.Read.Domain.Entities;
using System.Collections.Generic;

namespace M.Challenge.Read.Domain.Dtos
{
    public class PersonDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ethnicity { get; set; }
        public string Genre { get; set; }
        public List<Person> Filiation { get; set; }
        public List<Person> Children { get; set; }
        public string EducationLevel { get; set; }
    }
}

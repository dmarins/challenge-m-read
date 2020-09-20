using M.Challenge.Read.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace M.Challenge.Read.Domain.Contracts.Request
{
    public class PersonRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Ethnicity { get; set; }

        [Required]
        public string Genre { get; set; }

        public List<Person> Filiation { get; set; }

        public List<Person> Children { get; set; }

        [Required]
        public string EducationLevel { get; set; }
    }
}

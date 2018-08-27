using Curso.Udemy.Data.Converter;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Udemy.Data.Converters
{
    public class PersonConverter : IParser<PersonDTO, Person>, IParser<Person, PersonDTO>
    {
        public PersonDTO Parse(Person _origin)
        {
            if(_origin == null) return new PersonDTO();
            return new PersonDTO
            {
                Id = _origin.Id,
                FirstName = _origin.FirstName,
                LastName = _origin.LastName,
                Address = _origin.Address,
                Gender = _origin.Gender
            };
        }
  
        public List<PersonDTO> ParseList(List<Person> _origin)
        {
            if (_origin == null) return new List<PersonDTO>();
            return _origin.Select(item => Parse(item)).ToList();
        }

        public Person Parse(PersonDTO _origin)
        {
            if (_origin == null) return new Person();
            return new Person
            {
                Id = _origin.Id,
                FirstName = _origin.FirstName,
                LastName = _origin.LastName,
                Address = _origin.Address,
                Gender = _origin.Gender
            };
        }

        public List<Person> ParseList(List<PersonDTO> _origin)
        {
            if (_origin == null) return new List<Person>();
            return _origin.Select(item => Parse(item)).ToList();
        }
    }
}

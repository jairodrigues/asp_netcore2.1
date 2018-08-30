using Curso.Udemy.Data.Converters;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Repository;
using System.Collections.Generic;

namespace Curso.Udemy.Business.Implementations
{
    public class PersonBusinessImp : IPersonBusiness
    {

        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImp(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonDTO Create(PersonDTO person)
        {
            var result = _converter.Parse(person);
            result = _repository.Create(result);
            return _converter.Parse(result);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            
        }

        public List<PersonDTO> findAll()
        {
            return _converter.ParseList(_repository.findAll());
        }

        public PersonDTO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonDTO Update(PersonDTO person)
        {
            var _person = _converter.Parse(person);
            var result = _repository.Update(_person);
            return _converter.Parse(result);
        }

        public bool Exists(int? id)
        {
            return _repository.Exists(id);
        }

        public List<PersonDTO> FindById(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.findByName(firstName, lastName));
        }
    }
}

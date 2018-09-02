using Curso.Udemy.Data.Converters;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using Curso.Udemy.Repository;
using System;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

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

        public List<PersonDTO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
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

        public PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = @"select * from Person p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.FirstName like '%{name}%'";

            query = query + $" order by p.FirstName {sortDirection} offset {page} rows fetch next {pageSize} row only";

            string countQuery = @"select count(*) from Person p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.FirstName like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonDTO>
            {
                CurrentPage = page + 1,
                List = _converter.ParseList(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };

        }
    }
}

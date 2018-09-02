using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS.Utils;

namespace Curso.Udemy.Business

{
    public interface IPersonBusiness
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO FindById(int id);
        List<PersonDTO> FindAll();
        PersonDTO Update(PersonDTO person);
        void Delete(int id);
        List<PersonDTO> FindById(string firstName, string lastName);
        PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}

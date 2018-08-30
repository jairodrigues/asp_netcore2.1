using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);
        Person FindById(int id);
        List<Person> findAll();
        List<Person> findByName(string firstName, string lastName);
        Person Update(Person person);
        void Delete(int id);
        bool Exists(int? id);
    }
}

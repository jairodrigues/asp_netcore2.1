using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Udemy.Model;
using Curso.Udemy.Model.Context;

namespace Curso.Udemy.Repository.Implementations
{
    public class PersonRepositoryImp : IPersonRepository
    {
        private readonly SqlContext _context;

        public PersonRepositoryImp(SqlContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public void Delete(int id)
        {
            var result = _context.Person.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if(result != null)
                {
                    _context.Person.Remove(result);
                    _context.SaveChanges();

                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Person> findAll()
        {
            return _context.Person.ToList();
        }

        public Person FindById(int id)
        {
            return _context.Person.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _context.Person.SingleOrDefault(p => p.Id == person.Id);
            if(result != null)
            {
                try{
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public bool Exists(int? id)
        {
            return _context.Person.Any(p => p.Id.Equals(id));
        }

        public List<Person> findByName(string firstName, string lastName)
        {
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Person.Where(p => p.FirstName.Equals(firstName) && p.LastName.Equals(lastName)).ToList();
            }
            else if(string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Person.Where(p => p.LastName.Equals(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return _context.Person.Where(p => p.FirstName.Equals(firstName)).ToList();
            }
            else
            {
                return _context.Person.ToList();
            }


        }
    }
}

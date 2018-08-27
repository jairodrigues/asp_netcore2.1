using Curso.Udemy.Model;
using RestWithASPNETUdemy.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Repository.Generic

{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(int id);
        List<T> findAll();
        T Update(T item);
        void Delete(int id);
        bool Exists(int? id);
    }
}

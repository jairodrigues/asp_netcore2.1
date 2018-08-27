﻿using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Repository
{
    public interface UserRepository
    {
        Person Create(Person person);
        Person FindById(int id);
        List<Person> findAll();
        Person Update(Person person);
        void Delete(int id);
        bool Exists(int? id);
    }
}
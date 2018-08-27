﻿using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Business

{
    public interface IPersonBusiness
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO FindById(int id);
        List<PersonDTO> findAll();
        PersonDTO Update(PersonDTO person);
        void Delete(int id);
    }
}
﻿using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Repository
{
    public interface IUsersRepository
    {
        Users FindByLogin(string login);
       
    }
}

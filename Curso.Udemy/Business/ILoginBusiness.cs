using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Business
{
    public interface ILoginBusiness
    {
       object FindByLogin(User user);
    }
}


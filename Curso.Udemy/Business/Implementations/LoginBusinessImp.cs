using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.Udemy.Model;
using Curso.Udemy.Repository;

namespace Curso.Udemy.Business.Implementations
{
    public class LoginBusinessImp : ILoginBusiness
    {

        private IUserRepository _repository;


        public LoginBusinessImp(IUserRepository repository)
        {
            _repository = repository;

        }

        public object FindByLogin(User user)
        {
            throw new NotImplementedException();
        }
    } 
}

using Curso.Udemy.Model;
using Curso.Udemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Repository.Implementations
{
    public class UsersRepositoryImp : IUsersRepository
    {
        private readonly SqlContext _context;

        public UsersRepositoryImp(SqlContext context)
        {
            _context = context;
        }
        
        public Users FindByLogin(string login)
        {
            var result = _context.Users.SingleOrDefault(u => u.Login.Equals(login));
            return result;
        }
    }
}

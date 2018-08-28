using Curso.Udemy.Data.Converter;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Data.Converters
{
    public class UsersConverter : IParser<UsersDTO, Users>, IParser<Users, UsersDTO>
    {
        public Users Parse(UsersDTO origin)
        {
            if (origin == null) return new Users();
            return new Users
            {
                Login = origin.Login,
                AccessKey = origin.AccessKey
            };
        }

        public UsersDTO Parse(Users origin)
        {
            if (origin == null) return new UsersDTO();
            return new UsersDTO
            {
                Login = origin.Login,
                AccessKey = origin.AccessKey
            };
        }

        public List<Users> ParseList(List<UsersDTO> origin)
        {
            if (origin == null) return new List<Users>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UsersDTO> ParseList(List<Users> origin)
        {
            if (origin == null) return new List<UsersDTO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}

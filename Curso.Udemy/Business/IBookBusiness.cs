using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using System.Collections.Generic;

namespace Curso.Udemy.Business
{
    public interface IBookBusiness
    {
        BookDTO Create(BookDTO book);
        BookDTO FindById(int id);
        List<BookDTO> findAll();
        BookDTO Update(BookDTO book);
        void Delete(int id);
    }
}

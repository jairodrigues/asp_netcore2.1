using Curso.Udemy.Data.Converters;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using Curso.Udemy.Repository.Generic;
using System.Collections.Generic;

namespace Curso.Udemy.Business.Implementations
{
    public class BookBusinessImp : IBookBusiness
    {
        private IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImp(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookDTO Create(BookDTO book)
        {
            var _book = _converter.Parse(book);
            _book = _repository.Create(_book);
            return _converter.Parse(_book);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<BookDTO> findAll()
        {
            return _converter.ParseList(_repository.findAll());
        }

        public BookDTO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookDTO Update(BookDTO book)
        {
            var _book = _converter.Parse(book);
            _book = _repository.Update(_book);
            return _converter.Parse(_book);
        }


    }
}

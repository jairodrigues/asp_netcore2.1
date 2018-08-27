using Curso.Udemy.Data.Converter;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Udemy.Data.Converters
{
    public class BookConverter : IParser<BookDTO, Book>, IParser<Book, BookDTO>
    {
        public Book Parse(BookDTO _origin)
        {
            if (_origin == null) return new Book();
            return new Book
            {
                Id = _origin.Id,
                Author = _origin.Author,
                LaunchDate = _origin.LaunchDate,
                Price = _origin.Price,
                Title = _origin.Title
            };

        }

        public BookDTO Parse(Book _origin)
        {
            if (_origin == null) return new BookDTO();
            return new BookDTO
            {
                Id = _origin.Id,
                Author = _origin.Author,
                LaunchDate = _origin.LaunchDate,
                Price = _origin.Price,
                Title = _origin.Title
            };
        }

        public List<Book> ParseList(List<BookDTO> _origin)
        {
            if (_origin == null) return new List<Book>();
            return _origin.Select(item => Parse(item)).ToList();
        }

        public List<BookDTO> ParseList(List<Book> _origin)
        {
            if (_origin == null) return new List<BookDTO>();
            return _origin.Select(item => Parse(item)).ToList();
        }
    }
}

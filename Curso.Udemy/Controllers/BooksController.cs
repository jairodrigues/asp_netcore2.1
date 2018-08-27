using Curso.Udemy.Model;
using Curso.Udemy.Business;
using Curso.Udemy.Business.Implementations;
using Microsoft.AspNetCore.Mvc;
using Curso.Udemy.Data.DTO;
using Tapioca.HATEOAS;

namespace Curso.Udemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : Controller
    {

        private IBookBusiness _bookBusiness;


        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.findAll());
        }


        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }


        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]BookDTO book)
        {
            if (book == null) return NotFound();
            return new ObjectResult(_bookBusiness.Create(book));
        }


        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(int id, [FromBody]BookDTO book)
        {
            if (book == null) return BadRequest();
            var updatedBook = _bookBusiness.Update(book);
            if (updatedBook == null) return NoContent();
            return new ObjectResult(updatedBook);
        }


        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();

        }
    }
}

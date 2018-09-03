using Curso.Udemy.Business;
using Curso.Udemy.Data.DTO;
using Curso.Udemy.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Udemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : Controller
    {
        private IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost]
        [ProducesResponseType(typeof(byte []), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult GetPDFFile()
        {
            byte[] buffer = _fileBusiness.GetPDFFile();
            if (buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }              

            return new ContentResult();
        }

    }
}


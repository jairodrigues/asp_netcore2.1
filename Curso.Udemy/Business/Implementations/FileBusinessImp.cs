using System.IO;
using Curso.Udemy.Data.DTO;

namespace Curso.Udemy.Business.Implementations
{
    public class FileBusinessImp : IFileBusiness
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fulPath = path + "\\Other\\aspnet-life-cycles-events.pdf";
            return File.ReadAllBytes(fulPath);

        }
    }

}

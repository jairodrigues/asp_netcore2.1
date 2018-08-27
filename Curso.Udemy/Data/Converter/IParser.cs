using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Udemy.Data.Converter
{
    public interface IParser<Origin,Destiny>
    {
        Destiny Parse(Origin _origin);
        List<Destiny> ParseList(List<Origin> origin);
    }
}

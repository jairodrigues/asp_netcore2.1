using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace Curso.Udemy.Data.DTO
{
    [DataContract]
    public class BookDTO : ISupportsHyperMedia
    {
        [DataMember (Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2, Name="Autor")]
        public string Author { get; set; }
        [DataMember(Order = 3, Name = "Data")]
        public DateTime LaunchDate { get; set; }
        [DataMember(Order = 4, Name = "Preço")]
        public decimal Price { get; set; }
        [DataMember(Order = 5, Name = "Title")]
        public string Title { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}

using RestWithASPNETUdemy.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Udemy.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("Author")]
        public string Author { get; set; }

        [Column("LaunchDate")]
        public DateTime LaunchDate { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Title")]
        public string Title { get; set; }
    }
}


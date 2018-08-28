using Microsoft.EntityFrameworkCore;

namespace Curso.Udemy.Model.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext() { }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
        public DbSet<Person> Person { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }

    }
}

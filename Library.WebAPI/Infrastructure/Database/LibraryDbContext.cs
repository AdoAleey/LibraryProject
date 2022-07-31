using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Infrastructure.Database
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}

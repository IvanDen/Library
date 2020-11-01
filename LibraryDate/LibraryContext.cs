using LibraryDate.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDate
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patron> Patrons { get; set; }
    }
}

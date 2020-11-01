using LibraryDate.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDate
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutHistory> CheckoutHistorys { get; set; }
        public DbSet<LibraryBranch> LibraryBranchs { get; set; }
        public DbSet<BranchHours> BranchHours { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<LibraryAsset> LibraryAssets { get; set; }
        public DbSet<Hold> Holds { get; set; }
    }
}

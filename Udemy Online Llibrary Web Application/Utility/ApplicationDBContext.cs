using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyWebApplication.Models;

namespace UdemyWebApplication.Utility
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}

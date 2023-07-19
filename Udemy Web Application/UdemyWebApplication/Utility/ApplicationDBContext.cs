using Microsoft.EntityFrameworkCore;
using UdemyWebApplication.Models;

namespace UdemyWebApplication.Utility
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions <ApplicationDBContext> options) : base(options) 
		{
		}
		public DbSet<BookType> BookTypes { get; set; }	
		public DbSet<Book> Books { get; set; }
	}
}

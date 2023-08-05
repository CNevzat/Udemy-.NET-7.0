using System.Linq.Expressions;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Models
{
    public class BookRepository : Repository<Book>, IBookRepository // Önce somut sınıf inherit edilir
    {
        private ApplicationDBContext _applicationDBContext;
        public BookRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void Save()
        {
            _applicationDBContext.SaveChanges();
        }
        public void Update(Book book)
        {
            _applicationDBContext.Update(book);
        }
    }
}

using System.Linq.Expressions;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Models
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository // Önce somut sınıf inherit edilir
    {
        private ApplicationDBContext _applicationDBContext;
        public BookTypeRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void Save()
        {
            _applicationDBContext.SaveChanges();
        }
        public void Update(BookType bookType)
        {
            _applicationDBContext.Update(bookType);
        }
    }
}

using System.Linq.Expressions;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Models
{
    public class RentRepository : Repository<Rent>, IRentRepository // Önce somut sınıf inherit edilir
    {
        private ApplicationDBContext _applicationDBContext;
        public RentRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void Save()
        {
            _applicationDBContext.SaveChanges();
        }
        public void Update(Rent rent)
        {
            _applicationDBContext.Update(rent);
        }
    }
}

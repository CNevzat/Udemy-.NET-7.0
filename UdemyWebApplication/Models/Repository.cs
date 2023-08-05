using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _applicationDBContext;
        internal DbSet<T> dbSet; // dbSet = _applicationDBContext.BookTypes
        public Repository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            this.dbSet = _applicationDBContext.Set<T>();
            _applicationDBContext.Books.Include(k => k.BookType).Include(k => k.BookTypeID);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null) // Tek kayıt getirir
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.FirstOrDefault(); // Tek bir sonuç döndürmesi için 
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.ToList(); // dbSetten gelen tüm listeyi döndürür
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity); // Tek bir kaydı siler
        }

        public void RemoveInternal(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities); // Belirtilen aralığı siler
        }
    }
}

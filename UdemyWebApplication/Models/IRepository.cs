using System.Linq.Expressions;

namespace UdemyWebApplication.Models
{
    public interface IRepository<T> where T : class
    {
        //T --> BookType
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProps = null); //hazır yapı
        void Add(T entity);
        void Remove(T entity);
        void RemoveInternal(IEnumerable<T> entities);


    }
}

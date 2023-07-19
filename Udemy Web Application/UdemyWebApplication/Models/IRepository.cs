using System.Linq.Expressions;

namespace UdemyWebApplication.Models
{
    public interface IRepository <T> where T : class
    {
        //T --> BookType
        IEnumerable<T> GetAll ();
        T Get(Expression<Func<T, bool>> filter); //hazır yapı
        void Add(T entity);
        void Remove(T entity);
        void RemoveInternal (IEnumerable<T> entities);


    }
}

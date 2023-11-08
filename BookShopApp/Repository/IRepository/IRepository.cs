using System.Linq.Expressions;

namespace BookShopApp.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Expression<Func<T,bool>> filter);
        void Add(T entity);
        //void Update(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}

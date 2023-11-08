using BookShop.DataAccess.Data;
using BookShopApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookShopApp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db; 
            this.dbSet=_db.Set<T>();
        }
        void IRepository<T>.Add(T entity)
        {
           dbSet.Add(entity);
        }

        void IRepository<T>.Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

          public T GetById(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            return query.FirstOrDefault();

        }

        void IRepository<T>.RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}

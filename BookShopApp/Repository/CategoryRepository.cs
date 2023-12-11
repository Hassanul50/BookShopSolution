using BookShop.DataAccess.Data;
using BookShop.Models;
using BookShop.Models.Models;
using BookShopApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BookShopApp.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        
       

        public void update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}

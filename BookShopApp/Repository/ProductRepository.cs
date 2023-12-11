using BookShop.DataAccess.Data;

using BookShop.Models;
using BookShop.Models.Models;
using BookShopApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BookShopApp.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        
       

        public void update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}

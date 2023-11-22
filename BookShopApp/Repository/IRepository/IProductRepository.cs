
using BookShopApp.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookShopApp.Repository.IRepository
{
    public interface IProductRepository :IRepository<Product>
    {
        void update(Product obj);
        
    }
}

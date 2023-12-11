using BookShop.Models.Models;

namespace BookShopApp.Repository.IRepository
{
    public interface IProductRepository :IRepository<Product>
    {
        void update(Product obj);
        
    }
}

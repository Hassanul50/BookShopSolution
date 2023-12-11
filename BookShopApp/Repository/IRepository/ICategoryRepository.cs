using BookShop.Models;
using BookShop.Models.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookShopApp.Repository.IRepository
{
    public interface ICategoryRepository :IRepository<Category>
    {
        void update(Category obj);
        
    }
}

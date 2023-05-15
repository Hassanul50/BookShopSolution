using BookShopApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShopApp.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }     
    }
}

using Microsoft.AspNetCore.Mvc;

namespace BookShopApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using BookShop.DataAccess.Data;
using BookShopApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.DataAccess.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;      
        }
        public IActionResult Index()
        {
            List<Category> BookList = _db.Categories.ToList(); 

            return View(BookList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj) {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display order should  be diffferent from Category name Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index", "Category");
            }
            //else
            //{
            //    return View("please give some data");
            //}
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //Category categoryFromDb = _db.Categories.Find(id);//only for primary key 
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
          
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited succesfully";
                return RedirectToAction("Index", "Category");
            }
            //else
            //{
            //    return View("please give some data");
            //}
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category categoryFromDb = _db.Categories.Find(id);//only for primary key 
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryForDelete = _db.Categories.FirstOrDefault(u=>u.Id==id);
            if (categoryForDelete == null)
            {
                return NotFound();
            }

            
                _db.Categories.Remove(categoryForDelete);
                _db.SaveChanges();
                TempData["success"] = "Category Deleted succesfully";
            return RedirectToAction("Index", "Category");
            
            //else
            //{
            //    return View("please give some data");
            //}
            

        }

    }
}

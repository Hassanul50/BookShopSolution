using BookShop.DataAccess.Data;
using BookShop.Models;
using BookShop.Models.Models;


//using BookShopApp.Models;
using BookShopApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> BookList = _unitOfWork.Category.GetAll().ToList();

            return View(BookList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display order should  be diffferent from Category name Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
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
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category categoryFromDb = _db.Categories.Find(id);//only for primary key 
            Category? categoryFromDb = _unitOfWork.Category.GetById(u => u.Id == id);
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
                _unitOfWork.Category.update(obj);
                _unitOfWork.Save();
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
            Category? categoryFromDb = _unitOfWork.Category.GetById(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryForDelete = _unitOfWork.Category.GetById(u => u.Id == id); ;
            if (categoryForDelete == null)
            {
                return NotFound();
            }


            _unitOfWork.Category.Delete(categoryForDelete);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted succesfully";
            return RedirectToAction("Index", "Category");

            //else
            //{
            //    return View("please give some data");
            //}


        }

    }
}

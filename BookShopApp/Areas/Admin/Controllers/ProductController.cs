using BookShop.DataAccess.Data;

using BookShopApp.Models;
//using BookShopApp.Models;
using BookShopApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepo;
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> BookList = _unitOfWork.Product.GetAll().ToList();

            return View(BookList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index", "Product");
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
            //Product productFromDb = _db.Categories.Find(id);//only for primary key 
            Product? productFromDb = _unitOfWork.Product.GetById(u => u.Id == id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Edited succesfully";
                return RedirectToAction("Index", "Product");
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
            //Product productFromDb = _db.Categories.Find(id);//only for primary key 
            Product? productFromDb = _unitOfWork.Product.GetById(u => u.Id == id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? productForDelete = _unitOfWork.Product.GetById(u => u.Id == id); ;
            if (productForDelete == null)
            {
                return NotFound();
            }


            _unitOfWork.Product.Delete(productForDelete);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted succesfully";
            return RedirectToAction("Index", "Product");

            //else
            //{
            //    return View("please give some data");
            //}


        }

    }
}

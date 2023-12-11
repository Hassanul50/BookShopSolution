using BookShop.DataAccess.Data;

using BookShop.Models.Models;
using BookShop.Models.Models.ViewModel;

//using BookShopApp.Models;
using BookShopApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> BookList = _unitOfWork.Product.GetAll().ToList();
           
            return View(BookList);
        }
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString(),
            //});
            //ViewBag.CategoryList = categoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
            Product = new Product()

            };
            if(id== 0 || id == null)
            {
                return View(productVM);

            }
            else
            {
                productVM.Product=_unitOfWork.Product.GetById(u=>u.Id==id);
                return View(productVM);
            }

            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!= null) {
                    string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string ProductPath= Path.Combine(wwwRootPath, @"images\Product");
                    using (var fileStream = new FileStream(Path.Combine(ProductPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\Product" + fileName;

                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index", "Product");
            }

            else
            {

                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
           
            return View(productVM);
            }
    
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //Product productFromDb = _db.Categories.Find(id);//only for primary key 
        //    Product? productFromDb = _unitOfWork.Product.GetById(u => u.Id == id);
        //    //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product Edited succesfully";
        //        return RedirectToAction("Index", "Product");
        //    }
        //    //else
        //    //{
        //    //    return View("please give some data");
        //    //}
        //    return View();

        //}
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

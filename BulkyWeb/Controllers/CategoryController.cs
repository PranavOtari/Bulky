using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category ctg)
        {
            if(ctg.Name == ctg.DisplayOrder.ToString()) // custome validation
            {
                ModelState.AddModelError("name","Display order can not be exactly match with Name");
            }

            if (ctg.Name?.ToLower() == "test")
            {
                ModelState.AddModelError("","test is not valid value !");
            }

            if (ModelState.IsValid) 
            { 
               _db.Categories.Add(ctg);
               _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}

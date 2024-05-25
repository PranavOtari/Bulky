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

           

            if (ModelState.IsValid) 
            { 
               _db.Categories.Add(ctg);
               _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            
           //  Category? categoryFromDb1= _db.Categories.FirstOrDefault(c=>c.Id==id);
            Category? categoryFromDb = _db.Categories.Find(id); // works on primary key
            
            if(categoryFromDb == null) 
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category ctg)
        {
          
            if (ModelState.IsValid)
            {
                _db.Categories.Update(ctg);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}

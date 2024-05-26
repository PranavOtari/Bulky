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
                TempData["Success"] = "New Category created successfully !";
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
                TempData["Success"] = "Category updated successfully !";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

           
            Category? categoryFromDb = _db.Categories.Find(id); // works on primary key

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj= _db.Categories.Find(id);

            if (obj == null) 
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfully !";
            return RedirectToAction("Index");

             
            
        }


    }
}

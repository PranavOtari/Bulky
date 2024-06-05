using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(ctg);
                _categoryRepo.Save();
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


            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id); 


            if (categoryFromDb == null) 
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
                _categoryRepo.Update(ctg);
                _categoryRepo.Save();
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

           
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj= _categoryRepo.Get(u => u.Id == id);

            if (obj == null) 
            {
                return NotFound();
            }

            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["Success"] = "Category deleted successfully !";
            return RedirectToAction("Index");

             
            
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Repository.DBContext;
using Repository.Models;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BulkyDBContext _db;

        public CategoryController(BulkyDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var catergoryData = _db.Categories.ToList();
            return View(catergoryData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category data)
        {
            if (data.Name == data.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order and Name cannot be same");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            _db.Categories.Add(data);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //We can use Find only when checking using primary key
            var categoryFromDb = _db.Categories.Find(id);
            //We can use FirstOrDefault when checking using any field
            //var categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            //We can use Where when checking using any field and then to get only one record, we should apply FirstorDeafult
            //var categoryFromDb = _db.Categories.Where(x => x.Id == id).FirstOrDefault();
            
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _db.Categories.Update(data);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var category = _db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

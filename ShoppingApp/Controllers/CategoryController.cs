using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using System.Text.RegularExpressions;

namespace ShoppingApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            this._categoryRepo = db;   
        }
        public IActionResult Index()
        {
            var objCategoryList=_categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categories category)
        {
            if(Regex.IsMatch(category.CategoryName,@"\d"))
            {
                ModelState.AddModelError("CategoryName", "Category name should not contain numbers!");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["sucess"] = "Category added sucessfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id < 0)
            {
                return NotFound();
            }
            Categories? category = _categoryRepo.Get(x=>x.CategoryId==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Categories category)
        {
            if (Regex.IsMatch(category.CategoryName, @"\d"))
            {
                ModelState.AddModelError("CategoryName", "Category name should not contain numbers!");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["sucess"] = "Category edited sucessfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Categories? category = _categoryRepo.Get(x=>x.CategoryId==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Categories category)
        {
            if (category.CategoryId>=0)
            {
                _categoryRepo.Remove(category);
                _categoryRepo.Save();
                TempData["sucess"] = "Category deleted sucessfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}

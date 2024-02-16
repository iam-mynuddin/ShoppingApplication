using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using ShoppingApp.Utility;
using System.Text.RegularExpressions;

namespace ShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetails.ROLE_ADMIN)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categories category)
        {
            if (Regex.IsMatch(category.CategoryName, @"\d"))
            {
                ModelState.AddModelError("CategoryName", "Category name should not contain numbers!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["sucess"] = "Category added sucessfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Categories? category = _unitOfWork.Category.Get(x => x.CategoryId == id);
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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
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
            Categories? category = _unitOfWork.Category.Get(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Categories category)
        {
            if (category.CategoryId >= 0)
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["sucess"] = "Category deleted sucessfully!";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}

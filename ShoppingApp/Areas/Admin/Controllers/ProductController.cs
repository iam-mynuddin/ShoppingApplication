using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.DataAccess;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using ShoppingApp.Utility;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetails.ROLE_ADMIN)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objProductList = _unitOfWork.Product.GetAll().ToList();           
            return View(objProductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["sucess"] = "Product added sucessfully!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Product? Product = _unitOfWork.Product.Get(x => x.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }
        [HttpPost]
        public IActionResult Edit(Product Product)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(Product);
                _unitOfWork.Save();
                TempData["sucess"] = "Product edited sucessfully!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (product.ProductId >= 0)
            {
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
                TempData["sucess"] = "Product deleted sucessfully!";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}

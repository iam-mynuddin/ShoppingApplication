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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var objCompanyList = _unitOfWork.Company.GetAll().ToList();           
            return View(objCompanyList);
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
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Add(company);
                _unitOfWork.Save();
                TempData["success"] = "Company added successfully!";
                return RedirectToAction("Index", "Company");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Company? Company = _unitOfWork.Company.Get(x => x.Id == id);
            if (Company == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            });
            ViewBag.CategoryList = CategoryList;
            return View(Company);
        }
        [HttpPost]
        public IActionResult Edit(Company obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Company edited successfully!";
                return RedirectToAction("Index", "Company");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Company? Company = _unitOfWork.Company.Get(x => x.Id == id);
            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }
        [HttpPost]
        public IActionResult Delete(Company Company)
        {
            if (Company.Id >= 0)
            {
                _unitOfWork.Company.Remove(Company);
                _unitOfWork.Save();
                TempData["success"] = "Company deleted successfully!";
                return RedirectToAction("Index", "Company");
            }
            return View();
        }
    }
}

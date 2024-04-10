using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace ShoppingApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objList = _unitOfWork.Product.GetAll().ToList();
            return View(objList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult GetProductDetails(int id)
        {
            ShoppingCart obj = new()
            {
                Product = _unitOfWork.Product.Get(u => u.ProductId == id,strIncludeProp:"Category"),
                Count=1,
                ProductId=id
            };
            //var objResult = _unitOfWork.Product.GetProductWithCategory();
            return View("ProductDetails",obj);
        }
        [HttpPost]
        [Authorize]
        public IActionResult ProductDetails(ShoppingCart obj)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            obj.ApplicationUserId = userId;
            ShoppingCart cartFromDb=_unitOfWork.ShoppingCart.Get(x=>x.ApplicationUserId == userId&&x.ProductId==obj.ProductId);
            if(cartFromDb != null)
            {
                cartFromDb.Count+=obj.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
            _unitOfWork.ShoppingCart.Add(obj);
            }
            _unitOfWork.Save();
            TempData["success"] = "Added to cart!";
            return RedirectToAction(nameof(Index));
        }
    }
}

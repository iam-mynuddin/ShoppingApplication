using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using System.Diagnostics;

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
        public IActionResult GetProductDetails(int id)
        {
            var objResult = _unitOfWork.Product.Get(x => x.ProductId == id,"tblCategories");
            return View("ProductDetails",objResult);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ShoppingApp.DataAccess.Migrations;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using System.Security.Claims;

namespace ShoppingApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public ShoppingCartController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                OrderHeader = new(),
                ShoppingCartList=_unitofWork.ShoppingCart.GetMultiple(x=>x.ApplicationUserId==userId,
                strIncludeProp:"Product").ToList(),
            };
            foreach (var obj in ShoppingCartVM.ShoppingCartList)
            {
                obj.Price= GetPriceBasedOnQuantity(obj);
                ShoppingCartVM.OrderHeader.OrderTotal += (obj.Price*obj.Count);
            }
            return View(ShoppingCartVM);
        }
        public IActionResult Plus(int id)
        {
            var cartFromDb=_unitofWork.ShoppingCart.Get(x=>x.Id==id);
            cartFromDb.Count += 1;
            _unitofWork.ShoppingCart.Update(cartFromDb);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var cartFromDb = _unitofWork.ShoppingCart.Get(x => x.Id == id);
            _unitofWork.ShoppingCart.Remove(cartFromDb);
            _unitofWork.Save();
            TempData["success"] = "Deleted from cart";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int id)
        {
            var cartFromDb = _unitofWork.ShoppingCart.Get(x => x.Id == id);
            if(cartFromDb.Count == 1)
            {
                _unitofWork.ShoppingCart.Remove(cartFromDb);
                TempData["success"] = "Deleted from cart";

            }
            else
            {
                cartFromDb.Count -= 1;
                _unitofWork.ShoppingCart.Update(cartFromDb);
            }
            
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                OrderHeader = new(),
                ShoppingCartList = _unitofWork.ShoppingCart.GetMultiple(x => x.ApplicationUserId == userId,
                strIncludeProp: "Product").ToList(),
            };
            ShoppingCartVM.OrderHeader.ApplicationUser=_unitofWork.ApplicationUser.Get(x=>x.Id == userId);

            ShoppingCartVM.OrderHeader.Name=ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber=ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress=ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City=ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.PostalCode=ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
            ShoppingCartVM.OrderHeader.State=ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.ApplicationUserId=ShoppingCartVM.OrderHeader.ApplicationUser.Email;

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartVM);
        }
        private double GetPriceBasedOnQuantity(Models.ShoppingCart obj)
        {
            if (obj.Count <= 50)
            {
                return obj.Product.Price;
            }
            else
            {
                if(obj.Count <= 100)
                {
                    return obj.Product.Price50;
                }
                else
                {
                    return obj.Product.Price100;
                }
            }
        }
    }
}

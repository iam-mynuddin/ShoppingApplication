using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Controllers
{
    public class MyFirstController : Controller
    {
        // GET: MyFirstController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MyFirstController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyFirstController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyFirstController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyFirstController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyFirstController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyFirstController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyFirstController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost("MyFirst/MyFirstAPI")]
        public string MyFirstAPI()
        {
            return "Hello World";
        }
    }
}

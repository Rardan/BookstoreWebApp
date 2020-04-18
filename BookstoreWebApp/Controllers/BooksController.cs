using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    public class BooksController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
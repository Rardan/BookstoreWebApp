using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Helpers;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
                return View(await PaginatedList<Book>.CreateAsync(_bookService.GetFiltered(searchString),
                    pageNumber ?? 1, 20));
            }
            searchString = currentFilter;
            
            ViewData["CurrentFilter"] = searchString;
            int pageSize = 20;
            return View(await PaginatedList<Book>.CreateAsync(_bookService.GetAll(), pageNumber ?? 1, pageSize));
        }
    }
}
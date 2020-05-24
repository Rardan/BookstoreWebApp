using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Helpers;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using BookstoreWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IReviewService _reviewService;

        public BooksController(IBookService bookService, IAuthorService authorService, IReviewService reviewService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _reviewService = reviewService;
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

        // Check here
        public async Task<IActionResult> Details(int ?id)
        {
            int BookId = id ?? default(int);
            var book = _bookService.Get(BookId);
            var author = _authorService.Get(book.AuthorId);
            var reviews = _reviewService.Get(BookId);
            var details = new DetailsViewModel{Book = book, Reviews = reviews};
            // Can I pass vector here?
            //var reviews = _
            ViewData["Author"] = author;
            return View(details);
        }
    }
}
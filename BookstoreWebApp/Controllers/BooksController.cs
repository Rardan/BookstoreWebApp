using System;
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
            ViewData["NameAZSortParam"] = "";
            ViewData["NameZASortParam"] = "name_desc";
            ViewData["PriceLowHighSortParam"] = "Price";
            ViewData["PriceHighLowSortParam"] = "price_desc";
            ViewData["CurrentFilter"] = searchString;
            /* To do
             * Filter for rating when rating will be ready
             * 
             * 
             * 
             * 
            */

            if (searchString != null)
            {
                pageNumber = 1;
                return View(await PaginatedList<Book>.CreateAsync(_bookService.GetFiltered(searchString),
                    pageNumber ?? 1, 20));
            }
            else
            {
                searchString = currentFilter;
            }


            int pageSize = 20;
            return View(await PaginatedList<Book>.CreateAsync(_bookService.GetFilteredOther(currentFilter), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int ?id)
        {
            int BookId = id ?? default(int);
            var book = _bookService.Get(BookId);
            var author = _authorService.Get(book.AuthorId);
            var reviews = _reviewService.Get(BookId);
            var highrec = _bookService.Get4Books();
            var details = new DetailsViewModel { Book = book, Reviews = reviews, OtherBooks = highrec };
            ViewData["Author"] = author;
            return View(details);
        }


    }
}
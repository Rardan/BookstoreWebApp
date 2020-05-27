using System;
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
        private readonly IAuthorService _authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
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
            var books = _bookService.GetAll();

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
            switch (currentFilter)
            {
                case "name_desc":
                    books = _bookService.GetFilteredOther("name_desc");
                    break;
                case "Price":
                    books = _bookService.GetFilteredOther("Price");
                    break;
                case "price_desc":
                    books = _bookService.GetFilteredOther("price_desc");
                    break;
                default:
                    books = _bookService.GetFilteredOther("default");
                    break;
            }

            int pageSize = 20;
            return View(await PaginatedList<Book>.CreateAsync(books, pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int ?id)
        {
            int BookId = id ?? default(int);
            var book = _bookService.Get(BookId);
            var author = _authorService.Get(book.AuthorId);
            ViewData["Author"] = author;
            return View(book);
        }


    }
}
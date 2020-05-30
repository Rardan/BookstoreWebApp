using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Helpers;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using BookstoreWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var avgRating = _reviewService.GetAverageRating(BookId);
            var highrec = await _bookService.Get4Books();
            var details = new DetailsViewModel { Book = book, Reviews = reviews, OtherBooks = highrec, AverageRating = avgRating };
            ViewData["Author"] = author;
            return View(details);
        }

        public async Task<IActionResult> Reviews(int ?id)
        {
            int BookId = id ?? default(int);
            var book = _bookService.Get(BookId);
            var reviews = _reviewService.Get(BookId);
            var details = new ReviewsViewModel
            {
                Book = book,
                Reviews = reviews
            };
            return View(details);
        }

        public async Task<IActionResult> AddReview(int ?id)
        {
            int BookId = id ?? default(int);
            var book = _bookService.Get(BookId);
            String description = Request.Query["description"];
            String rate = Request.Query["rate"].ToString();
            Decimal rateA = Decimal.Parse(rate);
            Review entity = new Review { Description = description, Rate = rateA, Book = book };
            try
            {
                await _reviewService.Add(entity);
            }
            catch(DbUpdateException e)
            {
                RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        


    }
}
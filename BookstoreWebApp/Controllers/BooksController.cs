using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookstoreWebApp.Data;
using BookstoreWebApp.Dtos;
using BookstoreWebApp.Helpers;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using BookstoreWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IReviewService _reviewService;
        private readonly IPublisherService _publisherService;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public BooksController(IBookService bookService, IAuthorService authorService, IReviewService reviewService, IMapper mapper,
                                IGenreRepository genreRepository, IPublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _reviewService = reviewService;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _publisherService = publisherService;
        }
        
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameAZSortParam"] = "";
            ViewData["NameZASortParam"] = "name_desc";
            ViewData["PriceLowHighSortParam"] = "Price";
            ViewData["PriceHighLowSortParam"] = "price_desc";
            ViewData["CurrentFilter"] = searchString;
            if (TempData["status"]?.ToString() == null)
                TempData["status"] = "none";
            ViewData["status"] = TempData["status"];
            
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
        
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["status"] = "none";
            var authors = await _authorService.GetAll();
            authors = authors.OrderBy(a => a.Name).ToList();
            ViewData["authors"] = new SelectList(authors, "Id", "Name");
            var genres = await _genreRepository.GetAll();
            ViewData["genres"] = new SelectList(genres, "Id", "Name");
            var publishers = await _publisherService.GetAll();
            ViewData["publishers"] = new SelectList(publishers, "Id", "Name");
            return View();
        }
        
        [Authorize(Roles = "Employee, Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            try
            {
                if (bookDto == null || !ModelState.IsValid)
                {
                    TempData["status"] = "error";
                    return RedirectToAction(nameof(Index));
                }

                var book = _mapper.Map<Book>(bookDto);
                if (book.Photo == null)
                {
                    book.Photo =
                        "https://firstfreerockford.org/wp-content/uploads/2018/08/placeholder-book-cover-default.png";
                }
                await _bookService.Add(book);
                TempData["status"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                TempData["status"] = "error";
                RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["status"] = "none";
            var authors = await _authorService.GetAll();
            authors = authors.OrderBy(a => a.Name).ToList();
            ViewData["authors"] = new SelectList(authors, "Id", "Name");
            var genres = await _genreRepository.GetAll();
            ViewData["genres"] = new SelectList(genres, "Id", "Name");
            var publishers = await _publisherService.GetAll();
            ViewData["publishers"] = new SelectList(publishers, "Id", "Name");
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound("wrong id");
            }
            return View(book);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public IActionResult Edit(Book updatedBook)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.Update(updatedBook);
                }
                catch (Exception)
                {
                    throw;
                }
                TempData["status"] = "success";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee, Admin")]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);

            TempData["status"] = "deleted";
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        [Authorize(Roles = "Employee, Admin")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound("wrong book id");
            }

            return View(book);
        }
    }
}
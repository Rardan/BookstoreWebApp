using BookstoreWebApp.Data;
using BookstoreWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookstoreDbContext _context;
        IWebHostEnvironment _appEnvironment;
        public BookController(BookstoreDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult IndexCase(String genreroute)
        {
            var stringBook = genreroute;
            switch(genreroute)
            {
                case "All":
                    return RedirectToAction("IndexAll");
                default:
                    return RedirectToAction("Index");
            }
        }

        public IActionResult IndexAll()
        {
            var allBooks = _context.Books.ToList();
            return View(allBooks);
        }

        public  IActionResult BookDetails(int? id)
        {
            
            int v2 = id ?? default(int);
            var books = _context.Books.ToList();
            var authors = _context.Authors.ToList();
            var book = books.ElementAt(v2);
            var author = authors.ElementAt(book.AuthorId);
            ViewData["Author"] = author;
            return View(book);
        }

        /// <summary>
        /// Standardowy widok aplikacji
        /// </summary>
        /// <returns>Widok dodawania produktów</returns>
        public IActionResult Create()
        {
            Book book;
            using (BookstoreDbContext cont = new BookstoreDbContext())
            { book = _context.Books.First(); }
            
            return View();
        }

        /// <summary>
        /// Stworzenie ksiazki w bazie danych na podstawie podstawowych parametrów z modelu
        /// </summary>
        /// <returns>Widok dodawania produktów</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Description,ISBN,Dating,Photo,AuthorId,GenreId,PublisherId")] Book newBook, IFormFile bookPhoto)
        {
            if (ModelState.IsValid)
            {
                if (bookPhoto == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string path = "/Files/" + newBook.Title +"/"+bookPhoto.FileName;
                    /*var path = Path.GetFileNameWithoutExtension(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", PhotoName.FileName));
                    var stream = new FileStream(path, FileMode.Create);
                    await PhotoName.CopyToAsync(stream);*/
                    newBook.Photo = path;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await bookPhoto.CopyToAsync(fileStream);
                    }
                }
                newBook.Genre = _context.Genres.Find(newBook.GenreId);
                newBook.Author = _context.Authors.Find(newBook.AuthorId);
                newBook.Publisher = _context.Publishers.Find(newBook.PublisherId);
                _context.Add(newBook);
                await _context.SaveChangesAsync();
            }
            return View();
        }
    }
}
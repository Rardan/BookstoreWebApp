using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _bookstoreDbContext;

        public BookRepository(BookstoreDbContext bookstoreDbContext)
        {
            _bookstoreDbContext = bookstoreDbContext;
        }

        public async Task<List<Book>> Books()
        {
            return await _bookstoreDbContext.Books
                .Include(a => a.Author)
                .Include(p => p.Publisher)
                .Include(g => g.Genre)
                .Include(s => s.Storage)
                .ToListAsync();
        }

        public async Task<ICollection<Book>> Get3Books()
        {
            return await _bookstoreDbContext.Books
                .Include(a => a.Author)
                .Include(p => p.Publisher)
                .Include(g => g.Genre)
                .Include(s => s.Storage)
                .Take(3).ToListAsync();
        }

        public Book GetBookById(int bookId) => _bookstoreDbContext.Books.FirstOrDefault(b => b.Id == bookId);
        public void IncreaseInStorage(int bookId)
        {
            var storage = _bookstoreDbContext.Storages.FirstOrDefault(i => i.BookId == bookId);

            if (storage != null)
            {
                storage.Amount++;
                _bookstoreDbContext.Update(storage);
                _bookstoreDbContext.SaveChanges();
            }
        }
        public async Task<List<Book>> GetFiltered(string searchString)
        {
            var bookByTitle = await _bookstoreDbContext.Books
                .Include(a => a.Author)
                .Include(p => p.Publisher)
                .Include(g => g.Genre)
                .Include(s => s.Storage)
                .Where(b => b.Title.Contains(searchString))
                .ToListAsync();
            
            var bookByIsbn = await _bookstoreDbContext.Books
                .Include(a => a.Author)
                .Include(p => p.Publisher)
                .Include(g => g.Genre)
                .Include(s => s.Storage)
                .Where(b => b.ISBN.Contains(searchString))
                .ToListAsync();
            return  bookByTitle.Count != 0 ? bookByTitle : bookByIsbn;
        }

        public void DecreaseInStorage(int bookId)
        {
            var storage = _bookstoreDbContext.Storages.FirstOrDefault(i => i.BookId == bookId);

            if (storage != null)
            {
                if (storage.Amount > 0)
                {
                    storage.Amount++;
                    _bookstoreDbContext.Update(storage);
                    _bookstoreDbContext.SaveChanges();
                }
            }
        }

        public bool Exists(int id)
        {
            return _bookstoreDbContext.Books.Any(b => b.Id == id);
        }

        public IEnumerable<Storage> Storages => _bookstoreDbContext.Storages
            .Include(b => b.Book).ThenInclude(a => a.Author);
    }
}

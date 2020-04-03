using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public class BookRepository
    {
        private readonly BookstoreDbContext _bookstoreDbContext;

        public BookRepository(BookstoreDbContext bookstoreDbContext)
        {
            _bookstoreDbContext = bookstoreDbContext;
        }

        public IEnumerable<Book> Books => _bookstoreDbContext.Books
            .Include(a => a.Author).Include(p => p.Publisher)
            .Include(g => g.Genre).Include(s => s.Storage);
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

        public IEnumerable<Storage> Storages => _bookstoreDbContext.Storages
            .Include(b => b.Book).ThenInclude(a => a.Author);
    }
}

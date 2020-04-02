using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebApp.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookstoreDBContext _context;

        public BookRepository(BookstoreDBContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetAll()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Storage)
                .ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Storage)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> Add(Book entity)
        {
            if (!(await _context.Publishers.AnyAsync(p => p == entity.Publisher)))
                _context.Publishers.Add(entity.Publisher);
            else
                _context.Publishers.Attach(entity.Publisher);
            if (!(await _context.Authors.AnyAsync(a => a == entity.Author)))
                _context.Authors.Add(entity.Author);
            else
                _context.BookAuthors.Attach(entity.Author);

            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Book> Update(Book entity)
        {
            if (!(await _context.Books.AnyAsync(b => b.Id == entity.Id)))
                throw new InvalidOperationException("Book does not exist");
            _context.Books.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Book> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                throw new InvalidOperationException("Book does not exist");
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}

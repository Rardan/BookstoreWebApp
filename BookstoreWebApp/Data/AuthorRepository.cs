using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookstoreDbContext _context;

        public AuthorRepository(BookstoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAll()
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                .ToListAsync();
        }

        public Author Get(int id)
        {
            return _context.Authors
                .Include(a => a.BookAuthors)
                .FirstOrDefault(a => a.Id == id);
        }

        public async Task<Author> Add(Author entity)
        {
            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author> Update(Author entity)
        {
            if (!(await _context.Authors.AnyAsync(a => a.Id == entity.Id)))
                throw new InvalidOperationException("Book does not exist");
            
            _context.Authors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Author> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                throw new InvalidOperationException("Author does not exist");
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public bool Exists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}

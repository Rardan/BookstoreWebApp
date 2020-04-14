using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebApp.Data
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookstoreDbContext _context;

        public PublisherRepository(BookstoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Publisher>> GetAll()
        {
            return await _context.Publishers.ToListAsync();
        }

        public Publisher Get(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Publisher> Add(Publisher entity)
        {
            _context.Publishers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Publisher> Update(Publisher entity)
        {
            if (!(await _context.Publishers.AnyAsync(p => p.Id == entity.Id)))
            {
                throw new InvalidOperationException("Book does not exist");
            }
            _context.Publishers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Publisher> Delete(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                throw new InvalidOperationException("Author does not exist");
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public bool Exists(int id)
        {
            return _context.Publishers.Any(p => p.Id == id);
        }
    }
}

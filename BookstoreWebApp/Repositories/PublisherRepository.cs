using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Repositories
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private readonly BookstoreDBContext _context;

        public PublisherRepository(BookstoreDBContext context)
        {
            _context = context;
        }
        public async Task<List<Publisher>> GetAll()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher> Get(int id)
        {
            return await _context.Publishers.FirstOrDefault(p => p.Id == id);
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
        }
    }
}

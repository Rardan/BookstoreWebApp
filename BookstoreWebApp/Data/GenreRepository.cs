using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreWebApp.Data
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookstoreDbContext _context;

        public GenreRepository(BookstoreDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Data
{
    public interface IGenreRepository
    {
        public Task<List<Genre>> GetAll();
    }
}
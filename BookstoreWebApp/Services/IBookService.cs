using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAll();
        Task<ICollection<Book>> Get3Books();
        Task<ICollection<Book>> Get4Books();
        Book Get(int id);
        Task Add(Book entity);
        void Update(Book entity);
        void Delete(int id);
        bool Exists(int id);
        Task<List<Book>> GetFiltered(string? searchString);
        Task<List<Book>> GetFilteredOther(string? filter);
    }
}
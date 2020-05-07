using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public interface IReviewService
    {
        Task<List<Book>> GetAll();
        Book Get(int id);
        Task<Book> Add(Review entity);
        Task<Book> Delete(int id);
        Task<List<Book>> GetFiltered(string? searchString);
    }
}
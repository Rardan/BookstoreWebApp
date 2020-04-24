using BookstoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public interface IBookRepository
    {
        List<Book> Books { get; }
        Task<ICollection<Book>> Get3Books();
        IEnumerable<Storage> Storages { get; }

        void DecreaseInStorage(int bookId);
        Book GetBookById(int bookId);
        void IncreaseInStorage(int bookId);
        bool Exists(int id);
        Task<List<Book>> GetFiltered(string searchString);
    }
}
using BookstoreWebApp.Models;
using System.Collections.Generic;

namespace BookstoreWebApp.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
        IEnumerable<Storage> Storages { get; }

        void DecreaseInStorage(int bookId);
        Book GetBookById(int bookId);
        void IncreaseInStorage(int bookId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task<List<Book>> GetAll()
        {
            return _bookRepository.Books;
        }


        public async Task<ICollection<Book>> Get3Books()
        {
            return await _bookRepository.Get3Books();
        }

        public async Task<ICollection<Book>> Get4Books()
        {
            return await _bookRepository.Get4Books();
        }

        public Book Get(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public async Task Add(Book entity)
        {
            await _bookRepository.Add(entity);
        }

        public void Update(Book entity)
        {
            _bookRepository.Update(entity);
        }

        public void Delete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            _bookRepository.Delete(book);
        }

        public bool Exists(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Book>> GetFiltered(string? searchString)
        {
            return await _bookRepository.GetFiltered(searchString);
        }

        public async Task<List<Book>> GetFilteredOther(string? filter)
        {
            return await _bookRepository.GetFilteredOther(filter);
        }
    }
}
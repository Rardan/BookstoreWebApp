using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Data
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();
        Author Get(int id);
        Task<Author> Add(Author entity);
        Task<Author> Update(Author entity);
        Task<Author> Delete(int id);
        bool Exists(int id);
        Task<List<Author>> GetFiltered(string? searchString);
    }
}

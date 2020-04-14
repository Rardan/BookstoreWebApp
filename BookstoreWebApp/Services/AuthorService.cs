using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<List<Author>> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public Author Get(int id)
        {
            return _authorRepository.Get(id);
        }

        public async Task<Author> Add(Author entity)
        {
            return await _authorRepository.Add(entity);
        }

        public async Task<Author> Update(Author entity)
        {
            return await _authorRepository.Update(entity);
        }

        public async Task<Author> Delete(int id)
        {
            return await _authorRepository.Delete(id);
        }

        public bool Exists(int id)
        {
            return _authorRepository.Exists(id);
        }
    }
}

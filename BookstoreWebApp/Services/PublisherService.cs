using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        public async Task<List<Publisher>> GetAll()
        {
            return await _publisherRepository.GetAll();
        }

        public Publisher Get(int id)
        {
            return _publisherRepository.Get(id);
        }

        public async Task<Publisher> Add(Publisher entity)
        {
            return await _publisherRepository.Add(entity);
        }

        public async Task<Publisher> Update(Publisher entity)
        {
            return await _publisherRepository.Update(entity);
        }

        public async Task<Publisher> Delete(int id)
        {
            return await _publisherRepository.Delete(id);
        }

        public bool Exists(int id)
        {
            return _publisherRepository.Exists(id);
        }
    }
}

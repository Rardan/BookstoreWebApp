using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Data
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAll();
        Publisher Get(int id);
        Task<Publisher> Add(Publisher entity);
        Task<Publisher> Update(Publisher entity);
        Task<Publisher> Delete(int id);
        bool Exists(int id);
    }
}

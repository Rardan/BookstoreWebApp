using BookstoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public interface IReviewRepository
    {
        List<Review> Reviews { get; }
        IEnumerable<Storage> Storages { get; }

        void DecreaseInStorage(int reviewId);
        Review GetReviewById(int reviewId);
        void IncreaseInStorage(int reviewId);
        bool Exists(int id);
        Task<List<Review>> GetFiltered(string searchString);
    }
}

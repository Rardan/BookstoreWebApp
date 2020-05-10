using BookstoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public interface IReviewRepository
    {
        List<Review> Reviews { get; }

        // TODO: change to find by bookID
        Review GetReviewById(int reviewId);
        Task<List<Review>> GetFiltered(string searchString);
    }
}

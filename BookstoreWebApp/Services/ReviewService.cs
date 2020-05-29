using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<Review>> GetAll()
        {
            return _reviewRepository.Reviews;
        }

        public List<Review> Get(int bookId)
        {
            return _reviewRepository.GetReviewsById(bookId);
        }

        public Task<Review> Add(Review entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Review>> GetFiltered(string? searchString)
        {
            return await _reviewRepository.GetFiltered(searchString);
        }
    }
}
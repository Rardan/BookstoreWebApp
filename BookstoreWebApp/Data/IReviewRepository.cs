using BookstoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public interface IReviewRepository
    {
        List<Review> Reviews { get; }

        List<Review> GetReviewsById(int bookId);
        Task<List<Review>> GetFiltered(string searchString);
        Decimal GetAverageRating(int bookId);
        Task<Review> Add(Review entity);
    }
}
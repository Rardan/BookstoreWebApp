using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private BookstoreDbContext _bookstoreDbContext;

        public ReviewRepository(BookstoreDbContext bookstoreDbContext)
        {
            _bookstoreDbContext = bookstoreDbContext;
        }

        public List<Review> Reviews => _bookstoreDbContext.Reviews.Include(b => b.Book).Include(d => d.Description).Include(r => r.Rate).ToList();

        public async Task<List<Review>> GetFiltered(string searchString)
        {
            var reviewByBookAuthor = await _bookstoreDbContext.Reviews
                .Include(r => r.Rate)
                .Include(d => d.Description)
                .Where(b => b.Book.Author.Name.Contains(searchString))
                .ToListAsync();

            var reviewByBookTitle = await _bookstoreDbContext.Reviews
                .Include(r => r.Rate)
                .Include(d => d.Description)
                .Where(b => b.Book.Title.Contains(searchString))
                .ToListAsync();
            return reviewByBookAuthor.Count != 0 ? reviewByBookAuthor : reviewByBookTitle;
        }

        public List<Review> GetReviewsById(int bookId) => _bookstoreDbContext.Reviews.Where(r => r.Book.Id == bookId).ToList();

        public async Task<Review> Add(Review entity)
        {
            _bookstoreDbContext.Add(entity);
            await _bookstoreDbContext.SaveChangesAsync();
            return entity;
        }

        public Decimal GetAverageRating(int bookId)
        {
            Decimal counter = 0;
            Decimal averageRating = 0;
            List<Review> reviewList = GetReviewsById(bookId);
            foreach(var item in reviewList)
            {
                averageRating += item.Rate;
                counter++;
            }
            try
            {
                averageRating /= counter;
            }
            catch(Exception e)
            {
                averageRating = 0;
            }
            return averageRating;
        }
    }
}
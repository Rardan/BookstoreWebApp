﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAll();
        List<Review> Get(int bookId);
        Task<Review> Add(Review entity);
        Task<List<Review>> GetFiltered(string? searchString);

        Decimal GetAverageRating(int bookId);
    }
}
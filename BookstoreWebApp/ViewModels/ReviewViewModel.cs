using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.ViewModels
{
    public class ReviewsViewModel
    {
        public Book Book { get; set; }
        public List<Review> Reviews { get; set; }

    }
}
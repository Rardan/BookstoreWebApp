using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Models;

namespace BookstoreWebApp.ViewModels
{
    public class DetailsViewModel
    {
        public Book Book { get; set; }
        public List<Review> Reviews { get; set; }

        public Task<ICollection<Book>> OtherBooks { get; set; }
    }
}
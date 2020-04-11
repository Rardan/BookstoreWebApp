using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }

        
        public string Dating { get; set; }
        public string Photo { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public Publisher Publisher { get; set; }
        public Storage Storage { get; set; }
    }
}

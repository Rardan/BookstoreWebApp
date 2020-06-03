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
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public Publisher Publisher { get; set; }
        public Storage Storage { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}

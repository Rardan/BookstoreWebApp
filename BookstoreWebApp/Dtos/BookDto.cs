using System.ComponentModel.DataAnnotations;

namespace BookstoreWebApp.Dtos
{
    public class BookDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Dating is required")]
        public string Dating { get; set; }
        [Display(Name = "URL for book cover")]
        public string Photo { get; set; }
        [Required(ErrorMessage = "Author is required")]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Publisher is required")]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
    }
}
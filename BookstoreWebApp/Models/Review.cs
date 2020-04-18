using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }

        public Book Book { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }

        public Book Book { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
